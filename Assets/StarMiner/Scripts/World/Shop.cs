using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public Player player;
    public PlayerInteractions playerInteractions;
    public Planet planet;

    public UI HUD;

    public GameObject planetIcon;
    public Text planetName;

    public Text ironPrice;
    public Text goldPrice;
    public Text diamondPrice;
    public Text crystalPrice;
    public Text unknownPrice;

    public Text ironAmount;
    public Text goldAmount;
    public Text diamondAmount;
    public Text crystalAmount;
    public Text unknownAmount;

    public Text credits;
    public Text fuel;
    public Text maxFuel;
    public Text shields;
    public Text maxShields;


    void Start() {
        player = FindObjectOfType<Player>().GetComponent<Player>();
        playerInteractions = FindObjectOfType<Player>().GetComponent<PlayerInteractions>();
        gameObject.SetActive(false);
    }

    public void SellIron() {
        player.credits += planet.ironPrice * playerInteractions.cargo[0];
        playerInteractions.cargo[0] = 0;
        updateShop();
        FindObjectOfType<AudioManager>().Play("Trade");
    }

    public void SellGold() {
        player.credits += planet.goldPrice * playerInteractions.cargo[1];
        playerInteractions.cargo[1] = 0;
        updateShop();
        FindObjectOfType<AudioManager>().Play("Trade");
    }

    public void SellDiamond() {
        player.credits += planet.diamondPrice * playerInteractions.cargo[2];
        playerInteractions.cargo[2] = 0;
        updateShop();
        FindObjectOfType<AudioManager>().Play("Trade");
    }

    public void SellCrystal() {
        player.credits += planet.crystalPrice * playerInteractions.cargo[3];
        playerInteractions.cargo[3] = 0;
        updateShop();
        FindObjectOfType<AudioManager>().Play("Trade");
    }

    public void SellUnknown() {
        player.credits += planet.unknownPrice * playerInteractions.cargo[4];
        playerInteractions.cargo[4] = 0;
        updateShop();
        FindObjectOfType<AudioManager>().Play("Trade");
    }

    public void buyExtraFuel() {
        if(player.credits >= 500) {
            player.credits -= 500;
            player.maxFuel += 50;
            updateShop();
        }
    }

    public void buyExtendFuel() {
        if (player.credits >= 900) {
            player.credits -= 900;
            player.maxFuel += 50;
            updateShop();
        }
    }

    public void buyType2Laser() {
        if (player.credits >= 600) {
            player.credits -= 600;
            player.type2Laser = true;
            player.dualLaser = false;
            updateShop();
        }
    }

    public void buyDualLasers() {
        if (player.credits >= 1500) {
            player.credits -= 1500;
            player.dualLaser = true;
            player.type2Laser = false;
            updateShop();
        }
    }

    public void buyExtendCargo() {
        if (player.credits >= 900) {
            player.credits -= 900;
            playerInteractions.cargoSize += 3;
            updateShop();
        }
    }

    public void buyTARDISCargo() {
        if (player.credits >= 900) {
            player.credits -= 900;
            playerInteractions.cargoSize += 3;
            updateShop();
        }
    }

    public void repair() {
        if(player.credits > 0 && player.health != player.maxHealth) {
            float cost = (player.maxHealth - player.health) * 2.5f;
            if (player.credits >= cost) {
                player.credits -= cost;
                player.health = player.maxHealth;
            } else {
                player.health += (int)(player.credits / 2.5f);
                player.credits = 0;
            }
            FindObjectOfType<AudioManager>().Play("ShieldRestore");
            updateShop();
        }
    }

    public void refuel() {
        if (player.credits > 0 && player.fuel != player.maxFuel) {
            float cost = (player.maxFuel - player.fuel) * 2.5f;
            if (player.credits >= cost) {
                player.credits -= cost;
                player.fuel = player.maxFuel;
            } else {
                player.fuel += player.credits / 2.5f;
                player.credits = 0;
            }
            FindObjectOfType<AudioManager>().Play("Fuelup");
            updateShop();
        }
    }

    public void Launch() {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        HUD.DockToggle();
    }

    public void setUpShop() {

        planetIcon.GetComponent<SpriteRenderer>().sprite = planet.GetComponent<SpriteRenderer>().sprite;//Grab Sprite of Planet
        planetIcon.transform.localScale = planet.transform.localScale * 63;//Scale down sprite to fit into icon square

        planetName.text = planet.gameObject.name;

        ironPrice.text = planet.ironPrice.ToString();
        goldPrice.text = planet.goldPrice.ToString();
        diamondPrice.text = planet.diamondPrice.ToString();
        crystalPrice.text = planet.crystalPrice.ToString();
        unknownPrice.text = planet.unknownPrice.ToString();

        updateShop();
    }

    public void updateShop() {
        ironAmount.text = playerInteractions.cargo[0].ToString();
        goldAmount.text = playerInteractions.cargo[1].ToString();
        diamondAmount.text = playerInteractions.cargo[2].ToString();
        crystalAmount.text = playerInteractions.cargo[3].ToString();
        unknownAmount.text = playerInteractions.cargo[4].ToString();
        credits.text = player.credits.ToString("F2");
        fuel.text = player.fuel.ToString("F2");
        maxFuel.text = player.maxFuel.ToString();
        shields.text = player.health.ToString();
        maxShields.text = player.maxHealth.ToString();
    }
}
