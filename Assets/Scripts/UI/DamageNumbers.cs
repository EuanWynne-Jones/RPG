using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Attributes;

namespace RPG.UI
{

    public class DamageNumbers : MonoBehaviour
    {
        [SerializeField] public float endHeight;
        [SerializeField] public float speed;

        public List<Color32> colors;
        [SerializeField] public TMP_FontAsset textFont;
        public Color32 critColour;
        [SerializeField] public TMP_FontAsset critFont;
        bool hasStarted = false;
        float criticalRequirment = 5f;

        TMP_Text text;
        Vector3 startPos;

        void Start()
        {
            text = gameObject.GetComponentInChildren<TMP_Text>();
            text.gameObject.SetActive(false);
            startPos = text.gameObject.transform.position;
        }

        public void StartTextPopup(float damage)
        {

            if(gameObject.tag != "Player")
            {
                text.gameObject.transform.position = new Vector3(text.gameObject.transform.position.x, startPos.y, text.gameObject.transform.position.z);
                //Debug.Log("Starting Text popup");
                text.gameObject.SetActive(true);
                if (transform.parent.GetComponent<Health>() != null)
                {
                    text.text = damage.ToString();
                    if (damage >= criticalRequirment)
                    {
                        text.color = critColour;
                        text.font = critFont;
                    }
                    else
                    {
                        text.font = textFont;
                        int rand = Random.Range(0, colors.Count);
                        text.color = colors[rand];
                    }


                    hasStarted = true;
                }
            }
        }

        void Update()
        {
            if (hasStarted)
            {
                float newPos = Mathf.Lerp(text.gameObject.transform.position.y, text.gameObject.transform.position.y + endHeight / 40f, speed * Time.deltaTime);
                Color32 trans = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(text.color.a, 0f, speed * Time.deltaTime));

                text.color = trans;
                //Debug.Log(text.color.a);


                text.gameObject.transform.position = new Vector3(text.gameObject.transform.position.x, newPos, text.gameObject.transform.position.z);

                if (text.gameObject.transform.position.y >= endHeight)
                {
                    hasStarted = false;
                    text.gameObject.SetActive(false);
                }
            }
        }
    }

}