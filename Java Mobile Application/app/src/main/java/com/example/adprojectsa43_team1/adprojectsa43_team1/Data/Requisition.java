package com.example.adprojectsa43_team1.adprojectsa43_team1.Data;

import org.json.JSONArray;
import org.json.JSONObject;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import static com.example.adprojectsa43_team1.adprojectsa43_team1.Data.ConvertDateTime.convertJsonToDate;


/**
 * Created by weekiatquek on 24/1/17.
 */

public class Requisition extends java.util.HashMap<String,String>{

    //final static String host = "http://192.168.0.104/Team1WCF/Service.svc";
    final static String host ="http://172.17.252.170/Team1WCF/Service.svc";

    public Requisition(int requisitionID, int totalItem, String orderDate, String empFullName){
        put("requisitionID", String.valueOf(requisitionID));
        put("totalItem", String.valueOf(totalItem));
        put("orderDate", String.valueOf(orderDate));
        put("empFullName", String.valueOf(empFullName));
    }



    public Requisition(){}

    public static List<Requisition> getDeptRequisitions (String deptID) {
        List<Requisition> list = new ArrayList<Requisition>();
        //SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS");
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/DeptPendingRequisition/" + deptID);
            for (int i = 0; i < a.length(); i++) {
                JSONObject obj = a.getJSONObject(i);
                list.add(new Requisition(
                    obj.getInt("RequisitionID"),
                    obj.getInt("TotalItem"),
                    convertJsonToDate(obj.getString("OrderDate")),
                    obj.getString("FirstName") + " " +obj.getString("LastName")));
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return list;
    }

    /*public static String convertJsonToDate(String jSONString){
        String result = jSONString.replace("(", "").replace("/Date","");
        String results = result.substring(0, result.indexOf('+'));
        long time = Long.parseLong(results);
        Date myDate = new Date(time);

        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
        String date = sdf.format(myDate);
        return date;
    }*/
}

