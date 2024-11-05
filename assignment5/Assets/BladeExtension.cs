using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeExtension : MonoBehaviour
{
    float extend_speed = 0.2f;
    bool state = true;
    float minimum_scaling = 0;
    float maximum_scaling = 1;
    float interpolation_value;
    float scale_value;
    float initial_x;
    float initial_z;
    public GameObject blade;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        initial_x = transform.localScale.x;
        initial_z = transform.localScale.z;
        maximum_scaling = transform.localScale.y;
        scale_value = maximum_scaling;
        interpolation_value = maximum_scaling / extend_speed;
        state = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            interpolation_value = state ? -Mathf.Abs(interpolation_value) :
                      Mathf.Abs(interpolation_value);
            if ((state && interpolation_value < 0) || (!state && interpolation_value > 0))
            {
                audioSource.Play();
            }
        }
        scale_value += interpolation_value * Time.deltaTime;
        scale_value = Mathf.Clamp(scale_value,
                    minimum_scaling,
                    maximum_scaling);
        transform.localScale = new Vector3(initial_x,
                          scale_value,
                          initial_z);
        state = scale_value > 0;
        if (state)
        {
            blade.SetActive(true);
        }
        else
        {
            blade.SetActive(false);
        }
    }
}
