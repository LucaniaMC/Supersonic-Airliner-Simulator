using UnityEngine;

public class FogParallax : Parallax
{
    public SpriteRenderer sprite;


    // Update is called once per frame
    protected override void Update()
    {
        cameraDelta = cameraTransform.position - cameraStartPos;
        Vector3 parallaxOffset = cameraDelta * parallaxPercentage;

        newPos = startPos + new Vector3(parallaxOffset.x, parallaxOffset.y, 0) - originOffset;
        sprite.material.SetVector("_Parallax", (Vector2)newPos);
    }
}
