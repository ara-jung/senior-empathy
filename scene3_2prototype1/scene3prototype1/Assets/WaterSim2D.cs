using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSim2D : MonoBehaviour
{
    static System.Random random = new System.Random();
    /*Algorithm from https://www.diva-portal.org/smash/get/diva2:676516/FULLTEXT01.pdf
    Interactive 2D Particle-based Fluid Simulation
    for Mobile Devices
    Daniel Månsson
    */
    public Texture tex;
    public Texture insidebox;
    public Texture ousidebox;
    const float radius = .5f;
    const float collisionRadius = .1f;
    const float p0 = 1f; //base desity
    const float viscS = 0; // viscosity's linear dependence on the velocity
    const float viscB = 20f; //The viscosity's quadratic dependence on the velocity
    const float ddrk = 1f; //Stiffness used in DoubleDensityRelaxation
    const float ddrknear = 2f; //Near-stiffness used in DoubleDensityRelaxation
    const float friction = .1f;
    const float collisionSoftness = .5f;
    float timesincelast = 0f;
    static Vector2 gravity = new Vector2(0, 3f);
    static Vector2 collisionBox = new Vector2(collisionRadius, collisionRadius);
    Grid grid;
    DistanceField distanceField;
    //if have multiple this will fuck things up
    static List<Particle> particles = new List<Particle>();
    List<List<Particle>> neighbors = new List<List<Particle>>();
    // Start is called before the first frame update

    void Start()
    {
        grid = new Grid();
        distanceField = new DistanceField();
        distanceField.addRect(1, 1, 9, 9, true);
        distanceField.addRect(2, 5, 8, 6);



    }
    private class Grid
    {
        public void MoveParticle(Particle p)
        {


        }
        public List<Particle> PossibleNeighbors(Particle p)
        {
            return particles;

        }

    }
    void addParticle(float x, float y)
    {
        particles.Add(new Particle(new Vector2(5, 2)));
        neighbors.Add(new List<Particle>());

    }
    private class DistanceField
    {
        private Vector2 lastNormal = Vector2.zero;
        internal List<rRect> rects = new List<rRect>();
        internal class rRect
        {
            internal Vector2 center;
            internal Vector2 rradius;
            internal Boolean concave;
            internal rRect(Vector2 topleft, Vector2 bottomright, Boolean concave = false)
            {
                this.concave = concave;
                this.center = .5f * (topleft + bottomright);
                this.rradius = .5f * (bottomright - topleft);
            }

        }
        internal void addRect(float a, float b, float c, float d, bool concave = false)
        {
            rects.Add(new rRect(new Vector2(a, b), new Vector2(c, d), concave));
        }


        internal float GetDistance(Vector2 pos)
        {
            float mind = Mathf.Infinity;
            foreach (rRect rect in rects)
            {
                float dx = Mathf.Abs(rect.center.x - pos.x) - rect.rradius.x;
                float dy = Mathf.Abs(rect.center.y - pos.y) - rect.rradius.y;
                float rd = Mathf.Max(dx, dy);

                if ((rect.concave ? -rd : rd) < mind)
                {
                    if (dx > dy)
                    {


                        if (pos.x > rect.center.x)
                        {
                            lastNormal = new Vector2(-1, 0);

                        }
                        else
                        {
                            lastNormal = new Vector2(1, 0);
                        }

                    }
                    else
                    {
                        if (pos.y > rect.center.y)
                        {
                            lastNormal = new Vector2(0, -1);

                        }
                        else
                        {
                            lastNormal = new Vector2(0, 1);
                        }
                    }
                }
                if (rect.concave)
                {
                    rd *= -1;
                    lastNormal *= -1;
                }
                mind = Mathf.Min(mind, rd);


            }
            return -1f * mind;



        }

        internal Vector2 GetNormal(Vector2 pos)
        {
            return lastNormal;
        }
    }

    private class Particle
    {
        public Vector2 pos;
        public Vector2 prevpos;
        public Vector2 vel;
        //public int index;
        public Particle(Vector2 inpos)
        {
            pos = inpos;
            vel = Vector2.left * (float)(.1f * random.NextDouble()) + .1f * gravity;
            this.prevpos = inpos;
            //  this.index = index;
        }
    }
    void OnGUI()
    {
        if (Event.current.type.Equals(EventType.Repaint))
        {
            foreach (Particle p in particles)
            {
                GUI.DrawTexture(new Rect(simToScreen(p.pos - collisionBox), simToScreen(2 * collisionBox)), tex);
            }
            foreach (DistanceField.rRect rect in distanceField.rects)
            {
                Texture rtex;
                if (rect.concave)
                {
                    rtex = insidebox;

                }
                else
                {
                    rtex = ousidebox;
                }
                GUI.DrawTexture(new Rect(simToScreen(rect.center - rect.rradius), simToScreen(2 * rect.rradius)), rtex);

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        timesincelast += Time.deltaTime;
        if (timesincelast > .1f)
        {
            advance();
        }

    }
    void advance()
    {
        // Debug.Log(particles.Count);
        float timeStep = timesincelast;
        addParticle(4.5f+(float)random.NextDouble(), 2+ (float)random.NextDouble());
        timesincelast = 0;


        ApplyExternalForces(timeStep);
        ApplyViscosity(timeStep);
        AdvanceParticles(timeStep);
        UpdateNeighbors(timeStep);
        DanceDanceRevolution(timeStep);
        ResolveCollisions(timeStep);
        UpdateVelocity(timeStep);

    }
    void ApplyExternalForces(float timeStep)
    {
        foreach (Particle p in particles)
        {
            p.vel += (gravity * timeStep);
        }

    }
    void ApplyViscosity(float timeStep)
    {
        for (int i = 0; i < particles.Count; i++)
        {
            Particle p = particles[i];
            List<Particle> neighs = neighbors[i];
            foreach (Particle n in neighs)
            {
                Vector2 vpn = n.pos - p.pos;
                float vi = Vector2.Dot((p.vel - n.vel), vpn);
                if (vi > 0)
                {
                    float length = vpn.magnitude;
                    vi = vi / length;
                    float q = length / radius;
                    Vector2 I = .5f * timeStep * (1 - q) * (viscS * vi + viscB * vi * vi) * vpn;
                    p.vel -= I;

                }

            }
        }

    }
    void AdvanceParticles(float timeStep)
    {
        foreach (Particle p in particles)
        {
            p.prevpos = p.pos;
            p.pos += timeStep * p.vel;
            grid.MoveParticle(p);


        }
    }

    void UpdateNeighbors(float timeStep)
    {
        for (int i = 0; i < particles.Count; i++)
        {
            Particle p = particles[i];
            neighbors[i].Clear();
            foreach (Particle n in grid.PossibleNeighbors(p))
            {

                // if ((p.pos − n.pos).sqrMagnitude < radius * radius){
                if ((p.pos - n.pos).sqrMagnitude < radius * radius)
                {
                    neighbors[i].Add(n);
                }



            }


        }


    }
    //double density relaxation
    void DanceDanceRevolution(float timeStep)
    {
        for (int i = 0; i < particles.Count; i++)
        {
            Particle p = particles[i];
            float psum = 0;
            float pnear = 0;
            List<Particle> neighs = neighbors[i];
            foreach (Particle n in neighs)
            {
                float tempn = (p.pos - n.pos).magnitude;
                float q = (1f - tempn) / radius;
                psum += q * q;
                pnear += q * q * q;

            }
            psum = ddrk * (psum - p0);
            pnear = ddrknear * pnear;
            Vector2 delta = Vector2.zero;
            foreach (Particle n in neighs)
            {
                //redundant calculation (incl sqrt), optimize this
                float tempn = (p.pos - n.pos).magnitude;
                float q = (1f - tempn) / radius;
                Vector2 vpn = p.pos - n.pos;
                Vector2 D = 0.5f * timeStep * timeStep * (psum * q + pnear * q * q) * vpn;
                n.pos += D;
                delta -= D;
            }
            p.pos += delta;



        }
    }

    void ResolveCollisions(float timeStep)
    {
        foreach (Particle p in particles)
        {
            float distance = distanceField.GetDistance(p.pos);
            if (distance > -2 * collisionRadius)
            {

                Vector2 normal = distanceField.GetNormal(p.pos);
                Vector2 tangent = PerpendicularCCW(normal);
                tangent *= friction * Vector2.Dot(p.vel, tangent) * timeStep;
                p.pos -= tangent;
                p.pos -= collisionSoftness * (distance + collisionRadius) * normal;
                // Debug.Log(distance +" "+ normal.x +" "+  normal.y);

            }

        }

    }
    void UpdateVelocity(float timeStep)
    {
        foreach (Particle p in particles)
        {
            p.vel = (p.pos - p.prevpos) / timeStep * .7f;
        }

    }




    private Vector2 PerpendicularCCW(Vector2 v)
    {
        return new Vector2(-1f * v.y, v.x);
    }
    private Vector2 simToScreen(Vector2 inp)
    {
        return new Vector2(Screen.height / 10f * inp.x, Screen.height / 10f * inp.y);
    }
}
