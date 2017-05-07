package com.example.adprojectsa43_team1.adprojectsa43_team1.Data;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by cynthianyeint on 30/1/17.
 */

public class RetrievalSummary extends java.util.HashMap<String,String>{

    //final static String host = "http://192.168.0.104/Team1WCF/Service.svc";
    final static String host ="http://172.17.252.170/Team1WCF/Service.svc";

    public RetrievalSummary(int requisition_ItemID,int itemID, String itemNumber, String description, String bin,
                            int inStockQty, int totalRequestedQty, int totalRetrievedQty){
        put("requisition_ItemID", String.valueOf(requisition_ItemID));
        put("itemID", String.valueOf(itemID));
        put("itemNumber", itemNumber);
        put("description", description);
        put("bin", bin);
        put("inStockQty", String.valueOf(inStockQty));
        put("totalRequestedQty", String.valueOf(totalRequestedQty));
        put("totalRetrievedQty", String.valueOf(totalRetrievedQty));
    }

    public RetrievalSummary(int requisition_ItemID,int departmentID, int itemID, String departmentName, String itemNumber,
                            String description, String bin, int inStockQty, int retrievedQty,
                            int requestedQty){
        put("requisition_ItemID", String.valueOf(requisition_ItemID));
        put("departmentID", String.valueOf(departmentID));
        put("itemID", String.valueOf(itemID));
        put("departmentName", String.valueOf(departmentName));
        put("itemNumber", itemNumber);
        put("description", description);
        put("bin", bin);
        put("inStockQty", String.valueOf(inStockQty));
        put("retrievedQty", String.valueOf(retrievedQty));
        put("requestedQty", String.valueOf(requestedQty));
    }

    public RetrievalSummary(){

    }

    public static List<RetrievalSummary> getRetrievalSummaryList(){
        List<RetrievalSummary> list = new ArrayList<RetrievalSummary>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/getRetrievalSummaryList");
            for (int i = 0; i < a.length(); i++){
                JSONObject obj = a.getJSONObject(i);
                list.add(new RetrievalSummary(
                        obj.getInt("Requisition_ItemID"),
                        obj.getInt("ItemID"),
                        obj.getString("ItemNumber"),
                        obj.getString("Description"),
                        obj.getString("Bin"),
                        obj.getInt("InStockQty"),
                        obj.getInt("TotalRequestedQty"),
                        obj.getInt("TotalRetrievedQty")
                ));
            }
        }catch (Exception e){
            e.printStackTrace();
        }
        return list;
    }

    //need itemID
    public static List<RetrievalSummary> getRetrievalDeptAllocation(String itemID){
        List<RetrievalSummary> list = new ArrayList<RetrievalSummary>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/retrievalDeptAllocation/" + itemID);
            for (int i = 0; i< a.length(); i++){
                JSONObject obj = a.getJSONObject(i);
                list.add(new RetrievalSummary(
                        obj.getInt("Requisition_ItemID"),
                        obj.getInt("DepartmentID"),
                        obj.getInt("ItemID"),
                        obj.getString("DepartmentName"),
                        obj.getString("ItemNumber"),
                        obj.getString("Description"),
                        obj.getString("Bin"),
                        obj.getInt("InStockQty"),
                        obj.getInt("RetrievedQty"),
                        obj.getInt("RequestedQty")
                ));
            }
        }catch (Exception e){
            e.printStackTrace();
        }
        return list;
    }

    public static void updateRetrievalSummary(String departmentID, String itemID,String requisition_ItemID,String retrievedQty){
        JSONParser.postStream(host + "/updateRetrieval/"+departmentID+ "/"+ itemID + "/"+ requisition_ItemID+"/"+retrievedQty, departmentID.toString()); //need departmentID

    }


}
