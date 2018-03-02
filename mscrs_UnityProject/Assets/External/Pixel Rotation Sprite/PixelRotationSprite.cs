using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class PixelRotationSprite : MonoBehaviour {

    public SpriteRenderer targetRenderer;
    public SpriteRenderer rotationRenderer;
    public bool Visible = true;

    private MaterialPropertyBlock propBlock;

    private Sprite Sprite { get { return targetRenderer.sprite; } }

    private void OnEnable()
    {
        propBlock = new MaterialPropertyBlock();

        if (!rotationRenderer)
        {
            GameObject g = new GameObject("RotationSprite", typeof(SpriteRenderer));
            g.transform.parent = transform;
            g.transform.localPosition = Vector3.zero;
            g.transform.localScale = Vector3.one;
            g.transform.localRotation = Quaternion.identity;
            rotationRenderer = g.GetComponent<SpriteRenderer>();
            g.hideFlags = HideFlags.HideAndDontSave;
        }
        if (!targetRenderer)
        {
            targetRenderer = GetComponent<SpriteRenderer>();
        }
        if (targetRenderer) targetRenderer.enabled = false;
    }

    private void OnDisable()
    {
        if (rotationRenderer)
        {
            if (Application.isPlaying)
            {
                Destroy(rotationRenderer.gameObject);
            }
            else
            {
                DestroyImmediate(rotationRenderer.gameObject);
            }
        }
    }

	private void Update ()
    {
        if (!targetRenderer || !rotationRenderer || !targetRenderer.sprite || !targetRenderer.sharedMaterial) return;
        
        //Needed by the shader to do the rotation
        Vector2 center = Sprite.pivot + Sprite.rect.position;
        Vector4 rect = new Vector4(Sprite.rect.x, Sprite.rect.y, Sprite.textureRect.width, Sprite.textureRect.height);

        //Setup material property block
        rotationRenderer.GetPropertyBlock(propBlock);

        //Adjust the angle if the sprite is fliped
        bool flip = (targetRenderer.flipX == !targetRenderer.flipY) == !((targetRenderer.transform.lossyScale.x < 0) == !(targetRenderer.transform.lossyScale.y < 0));

        propBlock.SetFloat("_Angle", transform.rotation.eulerAngles.z * (flip ? -1 : 1));
        propBlock.SetFloat("_CenterX", center.x);
        propBlock.SetFloat("_CenterY", center.y);
        propBlock.SetVector("_SpriteRect", rect);
        rotationRenderer.SetPropertyBlock(propBlock);

        //Copy values from target renderer
        rotationRenderer.sprite = Sprite;
        rotationRenderer.color = targetRenderer.color;
        rotationRenderer.flipX = targetRenderer.flipX;
        rotationRenderer.flipY = targetRenderer.flipY;
        rotationRenderer.sharedMaterial = targetRenderer.sharedMaterial;
        rotationRenderer.sortingLayerID = targetRenderer.sortingLayerID;
        rotationRenderer.sortingOrder = targetRenderer.sortingOrder;
        rotationRenderer.enabled = Visible;

        //Reset rotation on rotation renderer
        Quaternion rot = rotationRenderer.transform.rotation;
        rot.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, 0);
        rotationRenderer.transform.rotation = rot;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PixelRotationSprite))]
public class PixelRotationSpriteEditor : Editor
{
    private PixelRotationSprite t;

    private void OnEnable()
    {
        t = target as PixelRotationSprite;
    }

    public override void OnInspectorGUI()
    {
        if (t.targetRenderer && t.targetRenderer.sharedMaterial.shader != Shader.Find("Sprites/Rotation"))
        {
            EditorGUILayout.HelpBox("Please assign a material that is using the Sprites/Rotation Shader to the sprite renderer.", MessageType.Error);
        }

    }
}
#endif