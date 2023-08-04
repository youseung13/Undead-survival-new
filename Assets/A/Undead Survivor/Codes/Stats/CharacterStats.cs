using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;


   [Header("Major stats")]
    public Stat strength; //1point increase damage by 1 and crit.power by 1%
    public Stat agility;  // 1 point increase evasion by 1% and crit.chance 1%
    public Stat intelligence; // 1point increase magic damage by 1, and magic resistance by 3
    public Stat vitality; // 1point increase health by 3 or 5 points;

   [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;  //default value 150%


   [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat health;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

   [Header("Magic stats")]
   public Stat fireDamage;
   public Stat iceDamage;
   public Stat lightingDamage;

   public bool isIgnited; //does damage over time
   public bool isChilled; // reduce armor by 20%
   public bool isShocked; // reduce accuracy by 20%

   [SerializeField] private float ailmentsDuration = 4f;

   private float ignitedTimer; 
   private float chilledTimer;
   private float shockedTimer;


   private float ignitedDamageCooldown = .3f; //틱주기
   private float ignitheDamageTimer;
   private int igniteDamage;
   [SerializeField] private GameObject shockStrikePrefab;
   private int shockDamage;


   public int currentHealth;

   public System.Action onHealthChanged;
   public bool isDead { get; private set;}

   protected virtual void Start()
   {
    critPower.SetDefaultValue(150);
    currentHealth = GetMaxHealthValue();
    fx = GetComponent<EntityFX>();

   }

   protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;
        ignitheDamageTimer -= Time.deltaTime;

        if (ignitedTimer < 0)
            isIgnited = false;

        if (chilledTimer < 0)
            isChilled = false;

        if (shockedTimer < 0)
            isShocked = false;

        if(isIgnited)
        ApplyIgniteDamage();
    }


    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        if(CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);

        //if inventory current weapon has fire effect
       // then DoMagicalDamage(_targetStats);
    }

    
    #region  Magical damage and ailements   
    public virtual void DoMagicalDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        int totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

        totalMagicalDamage = CheckTargetResistance(_targetStats, totalMagicalDamage);
        _targetStats.TakeDamage(totalMagicalDamage);


        if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
            return;


        AttemptToApplyAilementss(_targetStats, _fireDamage, _iceDamage, _lightingDamage);

    }

    private void AttemptToApplyAilementss(CharacterStats _targetStats, int _fireDamage, int _iceDamage, int _lightingDamage)
    {
        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < .5f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }

            if (Random.value < .5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (Random.value < .5f && _lightingDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
        }

        if (canApplyIgnite)
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .15f));

        if (canApplyShock)
            _targetStats.SetupShockStrikeDamage(Mathf.RoundToInt(_lightingDamage * .1f));


        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }

    public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
    {
        bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
        bool canApplyChill = !isIgnited && !isChilled && !isShocked;
        bool canApplyShock = !isIgnited && !isChilled;



      if(_ignite && canApplyIgnite)
      {
        isIgnited = _ignite;
        ignitedTimer = ailmentsDuration;

        fx.IgniteFxFor(ailmentsDuration);
      }



      if(_chill && canApplyChill)
      {
        isChilled = _chill;
        chilledTimer = ailmentsDuration;

        float _slowPercentage = .2f;
        GetComponent<Entity>().SlowEntityBy(_slowPercentage, ailmentsDuration);
        fx.ChillFxFor(ailmentsDuration);
      }
      


      if(_shock && canApplyShock)
      {
            if(!isShocked)
            {
                ApplyShock(_shock);
            }
            else
            {
                if (GetComponent<Player2>() != null)
                    return;

                HitNearestTargetWithShockStrike();
            }
        }

      isIgnited = _ignite;
      isChilled = _chill;
      isShocked = _shock;
    }

    public void ApplyShock(bool _shock)
    {
        if(isShocked)
        return;

        isShocked = _shock;
        shockedTimer = ailmentsDuration;

        fx.ShockFxFor(ailmentsDuration);
    }

    private void HitNearestTargetWithShockStrike()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15);

        float closetDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy2>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closetDistance)
                {
                    closetDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }

            if (closestEnemy == null)
                closestEnemy = transform;
        }

        if (closestEnemy != null)
        {
            GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);

            newShockStrike.GetComponent<ShockStrike_Controller>().Setup(shockDamage, closestEnemy.GetComponent<CharacterStats>());
        }
    }

     private void ApplyIgniteDamage()
    {
        if (ignitheDamageTimer < 0 )
        {
           // Debug.Log("Take burn damage" + igniteDamage);

            DecreaseHealthBy(igniteDamage);

            if (currentHealth < 0 && !isDead)
                Die();


            ignitheDamageTimer = ignitedDamageCooldown;
        }
    }

   public void SetupIgniteDamage(int _damage) => igniteDamage = _damage;
   public void SetupShockStrikeDamage(int _damage) => shockDamage = _damage;

   #endregion

    public virtual void TakeDamage(int _damage)
   {
    DecreaseHealthBy(_damage);

    GetComponent<Entity>().DamageImpact();
    fx.StartCoroutine("FlashFX");

     if(currentHealth <0  && !isDead)
        Die();
   }



    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if(onHealthChanged != null)
            onHealthChanged();

    }



   protected virtual void Die()
   {
    isDead = true;
   }


    #region  Stat calcultaions
   private int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        if(_targetStats.isChilled)
            totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * .8f);
        else
            totalDamage -= _targetStats.armor.GetValue();


        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);// 음수가 되어서 역으로 힐이 되지 않게
        return totalDamage;
    }

     private int CheckTargetResistance(CharacterStats _targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
        return totalMagicalDamage;
    }

   private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if(isShocked)
            totalEvasion +=20; //shock상태일떄 타겟의 회피를 올려주기위해

        if (Random.Range(0, 100) < totalEvasion)
        {
           return true;
        }
        return false;
    }
   private bool CanCrit()
   {
      int totalCriticalChance = critChance.GetValue() + agility.GetValue();

      if(Random.Range(0,100) <= totalCriticalChance)
      {
         return true;
      }

      return false;
   }

   private int CalculateCriticalDamage(int _damage)
   {
      float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;

      float critDamage = _damage * totalCritPower;

      return Mathf.RoundToInt(critDamage);//소수점 나오지않게 
   }


    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }

    #endregion
}

