using System;
using System.IO;
using System.Net;
using UnityEngine;

public class GetCountry : MonoBehaviour
{
    // Enter your ipstack API access key here.
    private string accessKey = "26f99c70-55f1-4247-8c19-d402ce626178";


    [Serializable]
    public class IpApiData
    {
        public string regionCode;

        public static IpApiData CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<IpApiData>(jsonString);
        }
    }



    private void Start()
    {
        // Get the IP address of the user.
        string ipAddress = GetIpAddress();
        Debug.Log(ipAddress);

        // Get the country of the user based on their IP address.
        string country = GetCountryLocate(ipAddress);

        // Output the country to the console.
        Debug.Log("User country: " + country);
    }

    private string GetIpAddress()
    {
        // Get the public IP address of the user using a web API.
        string apiUrl = "https://api.ipify.org";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string ipAddress = reader.ReadToEnd().Trim();
        reader.Close();
        response.Close();

        return ipAddress;
    }

    private string GetCountryLocate(string ipAddress)
    {
        // Use the ipstack API to get the country of the user based on their IP address.
        string apiUrl = "http://apiip.net/api/check?ip=" + ipAddress + "?access_key=" + accessKey;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd().Trim();
        reader.Close();
        response.Close();
        Debug.Log(jsonResponse);
        IpApiData ipApiData = IpApiData.CreateFromJSON(jsonResponse);

        Debug.Log(ipApiData.regionCode);
        // Parse the JSON response to get the user's country.


        return "";
    }
}