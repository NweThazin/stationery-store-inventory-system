package com.example.adprojectsa43_team1.adprojectsa43_team1.Data;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import static com.example.adprojectsa43_team1.adprojectsa43_team1.Data.ConvertDateTime.convertJsonToDate;
import static com.example.adprojectsa43_team1.adprojectsa43_team1.Data.ConvertDateTime.formatDate;

/**
 * Created by cynthianyeint on 20/1/17.
 */

public class Employee extends java.util.HashMap<String,String>{

    //final static String host = "http://192.168.0.104/Team1WCF/Service.svc";
    final static String host ="http://172.17.252.170/Team1WCF/Service.svc";

    public Employee(int employeeID, String firstName, String lastName,String fullName,String email, int phone, String address, int departmentID)
    {
        put("employeeID", String.valueOf(employeeID));
        put("firstName", firstName);
        put("lastName", lastName);
        put("fullName", fullName);
        put("email", email);
        put("phone", String.valueOf(phone));
        put("address", address);
        put("departmentID", String.valueOf(departmentID));
    }
    public Employee(int employeeID, String firstName, String lastName, String fullName, String email, int phone, String address, int departmentID,
                    String startDate, String endDate, String primaryRole, String delegatedRole)
    {
        put("employeeID", String.valueOf(employeeID));
        put("firstName", firstName);
        put("lastName", lastName);
        put("fullName", fullName);
        put("email", email);
        put("phone", String.valueOf(phone));
        put("address", address);
        put("departmentID", String.valueOf(departmentID));
        put("startDate", startDate);
        put("endDate", endDate);
        put("primaryRole", primaryRole);
        put("delegatedRole", delegatedRole);
    }

    public Employee(){}

    public static List<Employee> getDepartmentEmployee(String deptID){
        List<Employee> list = new ArrayList<Employee>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/DepEmployeeList/" + deptID);
            for (int i=0; i < a.length(); i++){
                JSONObject obj = a.getJSONObject(i);
                list.add(new Employee(
                        obj.getInt("EmployeeID"),
                        obj.getString("FirstName"),
                        obj.getString("LastName"),
                        obj.getString("FirstName") + " " +obj.getString("LastName"),
                        obj.getString("Email"),
                        obj.getInt("Phone"),
                        obj.getString("Address"),
                        obj.getInt("DepartmentID")));

            }
        }catch (Exception e){
            e.printStackTrace();
        }
        return list;
    }

    public static List<Employee> getDepartmentEmployeeForDelegation(String deptID){
        List<Employee> list = new ArrayList<Employee>();
        try{
            JSONArray a = JSONParser.getJSONArrayFromUrl(host + "/DepEmployeeListForDelegation/" + deptID);
            for (int i=0; i < a.length(); i++){
                JSONObject obj = a.getJSONObject(i);
                list.add(new Employee(
                        obj.getInt("EmployeeID"),
                        obj.getString("FirstName"),
                        obj.getString("LastName"),
                        obj.getString("FirstName") + " " +obj.getString("LastName"),
                        obj.getString("Email"),
                        obj.getInt("Phone"),
                        obj.getString("Address"),
                        obj.getInt("DepartmentID")));

            }
        }catch (Exception e){
            e.printStackTrace();
        }
        return list;
    }

    public static Employee getDeptRepresentative(String deptID){
        Employee emp = null;
        try{
            JSONObject obj = JSONParser.getJSONFromUrl(host+"/Department/Representative/" + deptID);
            emp = new Employee(
                    obj.getInt("EmployeeID"),
                    obj.getString("FirstName"),
                    obj.getString("LastName"),
                    obj.getString("FirstName") + " " +obj.getString("LastName"),
                    obj.getString("Email"),
                    obj.getInt("Phone"),
                    obj.getString("Address"),
                    obj.getInt("DepartmentID"));
        }catch (Exception e){
            e.printStackTrace();
        }
        return emp;
    }

    public static Employee getDeptDelegatedHead(String deptID){
        Employee emp = null;
        try{
            JSONObject obj = JSONParser.getJSONFromUrl(host + "/Department/DelegatedHead/" + deptID);
            emp = new Employee(
                    obj.getInt("EmployeeID"),
                    obj.getString("FirstName"),
                    obj.getString("LastName"),
                    obj.getString("FirstName") + " " +obj.getString("LastName"),
                    obj.getString("Email"),
                    obj.getInt("Phone"),
                    obj.getString("Address"),
                    obj.getInt("DepartmentID"),
                    convertJsonToDate(obj.getString("StartDate")),
                    convertJsonToDate(obj.getString("EndDate")),
                    obj.getString("PrimaryRole"),
                    obj.getString("DelegatedRole"));
        }catch (Exception e){
            e.printStackTrace();
        }
        return  emp;
    }

    public static void assignDeptRep(String employeeID){
        Log.i("assignID", employeeID);
        JSONParser.postStream(host + "/AssignRep/" +employeeID.toString(),employeeID.toString());
    }

    public static void removeDeptRep(String employeeID){
        JSONParser.postStream(host + "/RemoveRep/" +employeeID.toString(),employeeID.toString());
    }

    public static void assignDeptDelegateHead(String employeeID, String start, String end){
        JSONParser.postStream(host + "/assignDeptDelegateHead/" + employeeID + "/" + formatDate(start) + "/" + formatDate(end), employeeID.toString());
    }

    public static void removeDeptDelegateHead(String employeeID){
        Log.i("removeID", employeeID);
        JSONParser.postStream(host + "/removeDeptDelegateHead/" + employeeID, employeeID.toString());
    }

}
