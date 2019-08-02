using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveLoadSystem
{

    public static void SaveGold(int gold,int zone) // zone 0 = mainmenu ; 1 = lvl
    {
        if (File.Exists(Application.persistentDataPath + "/gold.ILTD")&& zone == 1)
        {
            string saveString = File.ReadAllText(Application.persistentDataPath + "/gold.ILTD");

            GainedGold loadedGainedGold = JsonUtility.FromJson<GainedGold>(saveString);

            GainedGold gainedGold = new GainedGold
            {
                goldAmount = gold + loadedGainedGold.goldAmount,
            };
            string json = JsonUtility.ToJson(gainedGold);
            File.WriteAllText(Application.persistentDataPath + "/gold.ILTD", json);
            //READ
            GainedGold loadGainedGold = JsonUtility.FromJson<GainedGold>(json);
            Debug.Log("Found more gold !");
            Debug.Log("Total gold : " + loadGainedGold.goldAmount);
        }
        else
        {

            if (zone == 1)
            {
                Debug.Log("No previous gold found :C");
            }
            //SAVE
            GainedGold gainedGold = new GainedGold
            {
                goldAmount = gold,
            };
            string json = JsonUtility.ToJson(gainedGold);
            File.WriteAllText(Application.persistentDataPath + "/gold.ILTD", json);
            //READ
            GainedGold loadGainedGold = JsonUtility.FromJson<GainedGold>(json);
            Debug.Log(loadGainedGold.goldAmount);
        }   

    }
    public static int LoadGold()
    {

        if (File.Exists(Application.persistentDataPath + "/gold.ILTD"))
        {
            string saveString = File.ReadAllText(Application.persistentDataPath + "/gold.ILTD");

            GainedGold gainedGold = JsonUtility.FromJson<GainedGold>(saveString);
            return gainedGold.goldAmount;
        } else
        {
            Debug.Log("No file found");
            return 0;
        }

    }
    public static void SaveSkillsLevels(int[] skills)
    {
        SkillsLevels skillslevels = new SkillsLevels
        {
            skillsLevels = skills,
        };
        string json = JsonUtility.ToJson(skillslevels);
        File.WriteAllText(Application.persistentDataPath + "/SkillsLevel.ILTD",json);

        SkillsLevels loadSkillsLevels = JsonUtility.FromJson<SkillsLevels>(json);
        Debug.Log("skill 0 : "+loadSkillsLevels.skillsLevels[0]);
        Debug.Log("skill 1 : " + loadSkillsLevels.skillsLevels[1]);
        Debug.Log("skill 2 : " + loadSkillsLevels.skillsLevels[2]);
        Debug.Log("skill 3 : " + loadSkillsLevels.skillsLevels[3]);
    }
    public static int[] LoadSkillsLevels()
    {
        if(File.Exists(Application.persistentDataPath + "/SkillsLevel.ILTD"))
        {
            string saveString = File.ReadAllText(Application.persistentDataPath + "/SkillsLevel.ILTD");

            SkillsLevels skillslevels = JsonUtility.FromJson<SkillsLevels>(saveString);
            return skillslevels.skillsLevels;
        }else
        {
            return new int[] { 0, 0, 0, 0 };
        }
    }
    public static void SavePlayerData(int _health, int _damage, float _shotspd, float _shotscatter)
    {
        PlayerData playerdata = new PlayerData
        {
            health = _health,
            damage = _damage,
            spd = _shotspd,
            scatter = _shotscatter,
        };
        string json = JsonUtility.ToJson(playerdata);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.ILTD", json);

        PlayerData loadPlayerdata = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("health : " + loadPlayerdata.health);
        Debug.Log("damage : " + loadPlayerdata.damage);
        Debug.Log("shotSpd : " + loadPlayerdata.spd);
        Debug.Log("scatter : " + loadPlayerdata.scatter);
    }
    public static float[] LoadPlayerData()
    {
        if(File.Exists(Application.persistentDataPath + "/PlayerData.ILTD"))
        {
            string saveString = File.ReadAllText(Application.persistentDataPath + "/PlayerData.ILTD");

            PlayerData playerdata = JsonUtility.FromJson<PlayerData>(saveString);
            return new float[] { playerdata.health,playerdata.damage,playerdata.spd,playerdata.scatter };
        }else
        {
            Debug.Log("No file found");
            return new float[] { 3f,1f,2f,8f};
        }
        
    }
    private class GainedGold
    {

        public int goldAmount;
        
    }
    private class PlayerData
    {

        public int health,damage;
        public float spd,scatter;

    }
    private class SkillsLevels
    {
        public int[] skillsLevels;
    }
}
