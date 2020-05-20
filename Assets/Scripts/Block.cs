using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip HitSound = null;
    [SerializeField] GameObject blockSparkleVFX = null;
    [SerializeField] Sprite[] hitSprites = null;

    Level level;
    int timesHit = 0;

    private void Start()
    {
        CountBreakableBlocks();
    }
    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();

        if (CanBreakBlock())
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CanBreakBlock())
        {
            HandelHit();
        }
    }
    private void HandelHit()
    {
        timesHit++;

        int maxHits = hitSprites.Length + 1;

        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = Mathf.Clamp(timesHit - 1, 0, hitSprites.Length);
        
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite Missing from array on " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockSFX();

        level.BreakBlock();

        TriggerVFX();

        FindObjectOfType<GameSession>().AddToScore();
    }

    private void TriggerVFX()
    {
        GameObject vfx = Instantiate(blockSparkleVFX, transform.position, transform.rotation);
        Destroy(vfx, 2f);
    }

    private void PlayBlockSFX()
    {
        AudioSource.PlayClipAtPoint(HitSound, Camera.main.transform.position);

        Destroy(gameObject);
    }
    public bool CanBreakBlock()
    {
        if (tag == "Breakable")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
