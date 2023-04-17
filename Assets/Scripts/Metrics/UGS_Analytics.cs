using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;
using Random = UnityEngine.Random;

public class UGS_Analytics : MonoBehaviour
{
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void PLayerAttack(bool weaponMelee)
    {
        //Define Event Name
        String eventName = "playerAttack";
        
        //Process parameters
        string typeOfWeapon;
        if (weaponMelee)
        {typeOfWeapon = "melee";}
        else
        {typeOfWeapon = "ranged";}
        
        //Define Custom Parameters
        Dictionary<string, object> parameters =new Dictionary<string, object>
        {
            { "typeOfWeapon", typeOfWeapon },
        };
        
        //The event will get cached locally and sent during the next scheduled upload, within 1 minute
        AnalyticsService.Instance.CustomData(eventName, parameters);
        
        // You can call Events.Flush() to send the event immediately
        //AnalyticsService.Instance.Flush();
    }
    
    public void PlayerDeath(string killer, float horizontalPosition)
    {
        //Define Event Name
        String eventName = "playerDeath";
        
        //Process parameters
        float playerInitialPosition = -6.72f;
        horizontalPosition = Mathf.Abs(horizontalPosition) - playerInitialPosition;
        
        //Define Custom Parameters
        Dictionary<string, object> parameters =new Dictionary<string, object>
        {
            { "killer", killer },
            { "distanceFromStart", horizontalPosition }
        };
        
        //The event will get cached locally and sent during the next scheduled upload, within 1 minute
        AnalyticsService.Instance.CustomData(eventName, parameters);
        
        // You can call Events.Flush() to send the event immediately
        //AnalyticsService.Instance.Flush();
    }
    
    #region Plantilla Custom Events
    public void CustomEvent(string parameterValue)
    {
        //Define Event Name
        String eventName = "buttonPress";
        
        //Define Custom Parameters
        Dictionary<string, object> parameters =new Dictionary<string, object>
        {
            { "parameterName", parameterValue },
        };
        
        //The event will get cached locally and sent during the next scheduled upload, within 1 minute
        AnalyticsService.Instance.CustomData(eventName, parameters);
        
        // You can call Events.Flush() to send the event immediately
        AnalyticsService.Instance.Flush();
    }
    #endregion
}