using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleshot = false;
    public bool isSpeedBoostActive = false;
    public bool isShieldActive = false;

    public int lives = 3;

    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShootPrefab;
    [SerializeField] private GameObject _shieldGameobject;
    [SerializeField] private GameObject[] _engines;

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _fireRate = 0.25f;

    private float _canfie = 0.0f;

    private UIManager _UIManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private int _hitCount = 0;

    void Start()
    {
        // Current pos = New pos
        transform.position = new Vector3(0, 0, 0);

        // up dating lives UI
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_UIManager != null)
        {
            _UIManager.UpdateLives(lives);
        }
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager != null)
        {
            _spawnManager.StatrSpawnRoutines();
        }

        _audioSource = GetComponent<AudioSource>();

        _hitCount = 0;
    }

    void Update()
    {
        Movement();

        // if space hey is pressed 
        // spawn laser at player position

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {

        if (Time.time > _canfie)
        {
            _audioSource.Play();

            if (canTripleshot == true)
            {
                Instantiate(_tripleShootPrefab, transform.position, Quaternion.identity);
            }

            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.97f, 0), Quaternion.identity);
            }

            _canfie = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // if speed boost enabled
        // move 1.5x the normal speed
        // else move normal speeds
        if (isSpeedBoostActive == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

        }

        // if player on the y is greater than 0
        // set player position on the y to 0
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        // if player on the x is greater then 9.5
        // set player position on the x to 9.5 
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    public void Damage()
    {

        if (isShieldActive == true)
        {
            isShieldActive = false;
            _shieldGameobject.SetActive(false);
            return;
        }

        _hitCount++;

        if (_hitCount == 1)
        {
            // turn left engine_failure on
            _engines[0].SetActive(true);

        }
        else if (_hitCount == 2)
        {
            // turn right engine_failure on
            _engines[1].SetActive(true);

        }

        lives--;
        // update UI
        _UIManager.UpdateLives(lives);

        if (lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _UIManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }

    // enable powerups
    public void EnableShieldPowerupOn()
    {
        isShieldActive = true;
        _shieldGameobject.SetActive(true);
    }

    public void TripleShotPowerupOn()
    {
        canTripleshot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    // coroutine method to powerdown the powerups
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleshot = false;
    }

    public IEnumerator EnableShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isShieldActive = false;
    }
}
