using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ICe and Fire effect", menuName = "Data/Item Effect/Ice and Fire")]
public class IceAndFire_Effect : ItemEffect
{
   [SerializeField] GameObject iceAndFireprefab;

    public override void ExecuteEffect(Transform _respawnPosition)
    {
        Player2 player = PlayerManager.instance.player;


        bool thridAttack = player.primaryAttack.comboCounter ==2;

        if(thridAttack)
        {
            GameObject newIceAndFire = Instantiate(iceAndFireprefab, _respawnPosition.position, player.transform.rotation);
            newIceAndFire.GetComponent<IceandFIre_Controller>().SetDirection(_respawnPosition.position - player.transform.position);

           // newIceAndFire.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.facingDir,yVelocity * player.facingDir);
        }

    }


}
