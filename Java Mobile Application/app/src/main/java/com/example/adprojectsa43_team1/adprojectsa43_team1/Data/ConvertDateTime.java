package com.example.adprojectsa43_team1.adprojectsa43_team1.Data;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Created by cynthianyeint on 1/2/17.
 */

public class ConvertDateTime {

    public static String convertJsonToDate(String jSONString){
        String result = jSONString.replace("(", "").replace("/Date","");
        String results = result.substring(0, result.indexOf('+'));
        long time = Long.parseLong(results);
        Date myDate = new Date(time);

        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
        String date = sdf.format(myDate);
        return date;
    }

    public static String convertJsonToTime(String jSONString){
        String result;
        if(jSONString.contains("M")){
            result = jSONString.replace("PT", "").replace("H",":").replace("M","");
        }else{
            result = jSONString.replace("PT", "").replace("H",":00");
        }

        return result;
    }

    public static String formatDate(String dateString){
        String newDateString ="";
        Date initDate = null;
        try {
            initDate = new SimpleDateFormat("dd/MM/yyyy").parse(dateString);
        } catch (ParseException e) {
            e.printStackTrace();
        }
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
        newDateString = formatter.format(initDate);
        return newDateString;
    }

}
