using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WAVE : MonoBehaviour
{
    private Image image;
    public Text text;
    private float size = 1f; //���ϴ� ������
    public float speed; //Ŀ�� ���� �ӵ�

    private float time;
    private Vector2 originScale; //���� ũ��

    private Image[] images;

    private void Awake()
    {
        originScale = transform.localScale; //���� ũ�� ����
        image = GetComponent<Image>();
        images = GetComponentsInChildren<Image>();
        text = transform.GetChild(6).GetComponent<Text>();
    }


    private void OnEnable()
    {
        StartCoroutine(Up());
        StartCoroutine(FadeAway());
        
    }

    IEnumerator Up()
    {
        while (transform.localScale.x < size)
        {
            transform.localScale = originScale * (1f + time * speed);
            time += Time.deltaTime;

            if (transform.localScale.x >= size)
            {
                time = 0;
                break;
            }
            yield return null;
        }
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(2);

        while (image.color.a > 0)
        {

            var color = image.color;
            color.a -= (1.5f * Time.deltaTime);
            for (int i=0;i<images.Length;i++)
            {
                images[i].color = color;
                text.color = color;

            }
            
            yield return null;
        }
        gameObject.SetActive(false);
    }



    private void OnDisable()
    {

        gameObject.transform.localScale = originScale;
    }
}