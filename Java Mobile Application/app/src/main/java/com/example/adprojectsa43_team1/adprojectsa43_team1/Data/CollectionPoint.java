package com.example.adprojectsa43_team1.adprojectsa43_team1.Data;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

import static com.example.adprojectsa43_team1.adprojectsa43_team1.Data.ConvertDateTime.convertJsonToTime;

/**
 * Created by cynthianyeint on 23/1/17.
 */

public class CollectionPoint extends java.util.HashMap<String,String>{
   // final static String host = "http://192.168.0.104/Team1WCF/Service.svc";
    final static String host ="http://172.17.252.170/Team1WCF/Service.svc";

    public CollectionPoint(int collectionPointID, String name){
        put("collectionPointID", String.valueOf(collectionPointID));
        put("name", name);
    }

    public CollectionPoint(int collectionPointID, String name, String time, String fullName){
        put("collectionPointID", String.valueOf(collectionPointID));
        put("collectionPointName", name);
        put("time", time);
        put("storeclerkName", fullName);
    }

    public CollectionPoint(int collectionPointID, String name, String time, int departmentID, String departmentName, String fullName, int phone){
        put("collectionPointID", String.valueOf(collectionPointID));
        put("collectionPointName", name);
        put("time", time);
        put("departmentID", String.valueOf(departmentID));
        put("departmentName", departmentName);
        put("departmentRepName", fullName);
        put("phone", String.valueOf(phone));
    }

    public CollectionPoint(){

    }

    public static List<CollectionPoint> getCollectionPointList(){
        List<CollectionPoint> list = new ArrayList<CollectionPoint>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/CollectionPoints");

            for (int i =0; i< a.length(); i++){
                JSONObject obj = a.getJSONObject(i);

                //convert time
                String jsonTime = obj.getString("Time");
                String timeValue = convertJsonToTime(jsonTime);

                list.add(new CollectionPoint(
                    obj.getInt("CollectionPointID"),
                    obj.getString("Name"),
                    timeValue,
                    obj.getString("EmpFirstName") + " " + obj.getString("EmpLastName")
                ));
            }
        }catch (Exception e){
            e.printStackTrace();
        }
        return list;
    }

    public static List<CollectionPoint> getCollectionPointDeptList(String collectionPointID){
        List<CollectionPoint> list = new ArrayList<CollectionPoint>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/CollectionPoint/Departments/" + collectionPointID);

            for (int i =0; i< a.length(); i++){
                JSONObject obj = a.getJSONObject(i);

                //convert time
                String jsonTime = obj.getString("Time");
                String timeValue = convertJsonToTime(jsonTime);

                list.add(new CollectionPoint(
                        obj.getInt("CollectionPointID"),
                        obj.getString("Name"),
                        timeValue,
                        obj.getInt("DepartmentID"),
                        obj.getString("DepartmentName"),
                        obj.getString("EmpFirstName") + " " + obj.getString("EmpLastName"),
                        obj.getInt("Phone")
                ));
            }
        }catch (Exception e){
            e.printStackTrace();
        }
        return list;
    }


    public static CollectionPoint getDeptCurrentCollectionPoint(String deptID){
        CollectionPoint cp = null;
        try{
            JSONObject jo = JSONParser.getJSONFromUrl(host+"/Department/CollectionPoint/" + deptID);
            cp = new CollectionPoint(
                    jo.getInt("CollectionPointID"),
                    jo.getString("Name"));
        }catch (Exception e){
            e.printStackTrace();
        }
        return  cp;
    }

    public static void updateCollectionPoint(String collectionPointID, String deptID){
        JSONParser.postStream(host + "/UpdateCollectionPt/" + collectionPointID + "/"  + deptID, collectionPointID.toString());

    }

    /*public static String convertJsonToTime(String jSONString){
        String result;
        if(jSONString.contains("M")){
            result = jSONString.replace("PT", "").replace("H",":").replace("M","");
        }else{
            result = jSONString.replace("PT", "").replace("H",":00");
        }

        return result;
    }*/

}
