using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private Animator fadingAnimation;

    private void Start(){
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }
    void OnTriggerEnter(Collider other)
    {
       this.gameObject.GetComponent<Renderer>().enabled = true;
       this.fadingAnimation.Play("FadeIn");
       //StartCoroutine("waitForPlay");
    }


    IEnumerator waitForPlay()
    {
        yield return new WaitForSeconds(2.0f);
		this.gameObject.GetComponent<Renderer>().material.SetOverrideTag("RenderType", "");
        this.gameObject.GetComponent<Renderer>().material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
        this.gameObject.GetComponent<Renderer>().material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
        this.gameObject.GetComponent<Renderer>().material.SetInt("_ZWrite", 1);
        this.gameObject.GetComponent<Renderer>().material.DisableKeyword("_ALPHATEST_ON");
        this.gameObject.GetComponent<Renderer>().material.DisableKeyword("_ALPHABLEND_ON");
        this.gameObject.GetComponent<Renderer>().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        this.gameObject.GetComponent<Renderer>().material.renderQueue = -1;
    }
}
