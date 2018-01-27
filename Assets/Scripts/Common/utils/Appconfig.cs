using UnityEngine;
using System.Collections;

public class AppConfig 
{
    public const bool GAME_DEBUG = true;

	public static bool CAMERARIG_OR_FPSCONTROLLER = false;

	public const bool  USE_FIXED_FRAMERATE = true;

	public const string FOLDER_DATACONFIG = "dataconfig/";

	public const float PLAYER_TO_TARGET_DISTANCE = 1.8f;  //玩家到达目标点距离判定
	public const float PLAYER_TO_NPC_DISTANCE = 2.2f;  //玩家到达NPC距离判定

	public static string ENEMY_LAYER = "Enemy";

}
