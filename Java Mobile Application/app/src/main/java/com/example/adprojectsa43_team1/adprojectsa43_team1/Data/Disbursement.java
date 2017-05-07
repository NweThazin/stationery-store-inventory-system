package com.example.adprojectsa43_team1.adprojectsa43_team1.Data;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by cynthianyeint on 24/1/17.
 */

public class Disbursement extends java.util.HashMap<String, String>{


    //final static String host = "http://192.168.0.104/Team1WCF/Service.svc";
    final static String host ="http://172.17.252.170/Team1WCF/Service.svc";

    public Disbursement(int itemID,int disbursementID, int disbursement_ItemID, String description, int qty, String status, String itemNumber, String uom){
        put("itemID", String.valueOf(itemID));
        put("disbursementID", String.valueOf(disbursementID));
        put("disbursement_ItemID", String.valueOf(disbursement_ItemID));
        put("description", description);
        put("qty", String.valueOf(qty));
        put("status", String.valueOf(status));
        put("itemNumber", String.valueOf(itemNumber));
        put("uom", String.valueOf(uom));
    }

    public  Disbursement(){}

    public static List<Disbursement> getDisbursementList(String departmentID){
        List<Disbursement> list = new ArrayList<Disbursement>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/DisbursementList/" + departmentID);
            for (int i =0; i< a.length(); i++){
                JSONObject obj = a.getJSONObject(i);
                list.add(new Disbursement(
                        obj.getInt("ItemID"),
                        obj.getInt("DisbursementID"),
                        obj.getInt("Disbursement_ItemID"),
                        obj.getString("Description"),
                        obj.getInt("Qty"),
                        obj.getString("Status"),
                        obj.getString("ItemNumber"),
                        obj.getString("Uom")
                ));
            }
        }catch (Exception e){
            e.printStackTrace();
        }
        return list;
    }

    public static void updateDisbursement(String deptID,String disbursementID, String itemID, String Qty) {
        JSONParser.postStream(host + "/updateDisbursement/" + deptID + "/"+ disbursementID +"/" + itemID + "/" + Qty, Qty.toString());
    }

    public static void confirmDisbursement(String deptID){
        JSONParser.postStream(host + "/confirmDisbursement/" + deptID, deptID.toString());
    }

}
