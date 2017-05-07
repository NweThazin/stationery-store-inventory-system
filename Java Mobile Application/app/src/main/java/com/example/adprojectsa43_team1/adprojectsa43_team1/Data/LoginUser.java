package com.example.adprojectsa43_team1.adprojectsa43_team1.Data;

import android.util.Log;

import org.json.JSONObject;

/**
 * Created by cynthianyeint on 31/1/17.
 */

public class LoginUser extends java.util.HashMap<String, String>{

    //final static String host = "http://192.168.0.104/Team1WCF/Service.svc";
    final static String host ="http://172.17.252.170/Team1WCF/Service.svc";

    public LoginUser(int userID, String userName, String password, String primaryRole,
                     String delegatedRole, int employeeID, String startDate, String endDate){
        put("userID", String.valueOf(userID));
        put("userName", userName);
        put("password", password);
        put("primaryRole", primaryRole);
        put("delegatedRole", delegatedRole);
        put("employeeID", String.valueOf(employeeID));
        put("startDate", String.valueOf(startDate));
        put("endDate", String.valueOf(endDate));
    }

    public LoginUser (int userID, int employeeID, int departmentID, String departmentName, String departmentCode,
                      String primaryRole, String delegatedRole){
        put("userID", String.valueOf(userID));
        put("employeeID", String.valueOf(employeeID));
        put("departmentID", String.valueOf(departmentID));
        put("departmentName", departmentName);
        put("departmentCode", departmentCode);
        put("primaryRole", primaryRole);
        put("delegatedRole", delegatedRole);
    }

    public static LoginUser loggedInUser(){
        LoginUser  u = null;
        try{
            JSONObject obj = JSONParser.getJSONFromUrl(host + "/loggedInUser/Keri/Fiqmyj9$");
            u = new LoginUser(
                    obj.getInt("UserID"),
                    obj.getString("UserName"),
                    obj.getString("Password"),
                    obj.getString("PrimaryRole"),
                    obj.getString("DelegatedRole"),
                    obj.getInt("EmployeeID"),
                    obj.getString("StartDate"),
                    obj.getString("EndDate"));
        }catch (Exception e){
            e.printStackTrace();
        }
        return u;
    }

    public static LoginUser getLoginInfo(String username, String password){
        LoginUser u = null;
        try{
            JSONObject obj = JSONParser.getJSONFromUrl(host + "/loggedInUser/" + username + "/" + password);

            //JSONObject obj = JSONParser.getJSONFromUrl(host + "/loggedInUser/Amy/Daclos2@"); // primary role - depthead
           // JSONObject obj = JSONParser.getJSONFromUrl(host+"/loggedInUser/Lena/Klnozs3@"); //store clerk
            //JSONObject obj = JSONParser.getJSONFromUrl(host + "/loggedInUser/Teddy/Sahuvo2$"); // delegated - employee rep
            //JSONObject obj = JSONParser.getJSONFromUrl(host + "/loggedInUser/Brandi/Cubixr9@"); // delegated role - employee head
            //JSONObject obj = JSONParser.getJSONFromUrl(host + "/loggedInUser/Julian/Bmohjz8@"); // delegated role - employee rephead

            u = new LoginUser(
                    obj.getInt("UserID"),
                    obj.getInt("EmployeeID"),
                    obj.getInt("DepartmentID"),
                    obj.getString("DepartmentName"),
                    obj.getString("DepartmentCode"),
                    obj.getString("PrimaryRole"),
                    obj.getString("DelegatedRole")
            );
        }catch (Exception e){
            e.printStackTrace();
        }
        return u;
    }


}
