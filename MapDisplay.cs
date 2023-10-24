using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class MapDisplay : MonoBehaviour
{

    public Renderer texttureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

        public void DrawTexture(Texture2D texture)
        {

        texttureRender.sharedMaterial.mainTexture = texture;
        texttureRender.transform.localScale = new Vector3 (texture.width, 1, texture.height);

        }

        public void DrawMesh(MeshData meshData, Texture2D texture)
        {

            meshFilter.sharedMesh = meshData.CreateMesh ();
            meshRenderer.sharedMaterial.mainTexture = texture;

        }

}
