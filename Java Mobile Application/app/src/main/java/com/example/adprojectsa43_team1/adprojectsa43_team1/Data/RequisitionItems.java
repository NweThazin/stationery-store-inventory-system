package com.example.adprojectsa43_team1.adprojectsa43_team1.Data;

import android.util.Log;
import android.widget.Button;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by cynthianyeint on 25/1/17.
 */

public class RequisitionItems extends java.util.HashMap<String, String>{

    //final static String host = "http://192.168.0.104/Team1WCF/Service.svc";
    final static String host ="http://172.17.252.170/Team1WCF/Service.svc";

    public RequisitionItems(int requisitionID, int requisitionItemID, int itemID, int requestedQty, String description, String uom){
        put("requisitionID", String.valueOf(requisitionID));
        put("requisitionItemID", String.valueOf(requisitionItemID));
        put("itemID", String.valueOf(itemID));
        put("requestedQty", String.valueOf(requestedQty));
        put("description", description);
        put("uom",uom);
    }

    public RequisitionItems(){}

    public static List<RequisitionItems> getRequisitionItems(String requisitionID){
        List<RequisitionItems> list = new ArrayList<RequisitionItems>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/RequisitionItem/" + requisitionID);

            for (int i=0; i < a.length(); i++){
                JSONObject obj = a.getJSONObject(i);
                list.add(new RequisitionItems(
                        obj.getInt("RequisitionID"),
                        obj.getInt("RequisitionItemID"),
                        obj.getInt("ItemID"),
                        obj.getInt("RequestedQty"),
                        obj.getString("Description"),
                        obj.getString("UnitOfMeasure")));
            }
        }catch (Exception e){
            e.printStackTrace();
        }
        return list;
    }

    public static void approveRequisition(String requisitionID){
        JSONParser.postStream(host + "/ApproveRequisition/" + requisitionID, requisitionID.toString());
    }

    public static void rejectRequisition(String requisitionID, String reason){
        Log.i("reason",reason);
        JSONParser.postStream(host + "/RejectRequisition/" + requisitionID + "/" + reason, requisitionID.toString());
    }

}
