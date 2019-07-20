using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;

    // 0 = triple shot 1 = speed 2 = shield
    [SerializeField] private int _powerupID;

    [SerializeField] private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // access the player
            Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (player != null)
            {

                // enable triple shot
                if (_powerupID == 0)
                {
                    player.canTripleshot = true;
                    player.TripleShotPowerupOn();

                }
                else if (_powerupID == 1)
                {
                    // enable speed boost
                    player.isSpeedBoostActive = true;
                    player.SpeedBoostPowerupOn();

                }
                else if (_powerupID == 2)
                {
                    // enable shields
                    player.isShieldActive = true;
                    player.EnableShieldPowerupOn();

                }
            }

            Destroy(this.gameObject);

        }

    }
}
