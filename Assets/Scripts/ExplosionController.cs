using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    ParticleSystem ps;

    private void Awake()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    public void ChangeParticleSkin(Sprite skin)
    {
        ps.textureSheetAnimation.SetSprite(0, skin);
    }
}
