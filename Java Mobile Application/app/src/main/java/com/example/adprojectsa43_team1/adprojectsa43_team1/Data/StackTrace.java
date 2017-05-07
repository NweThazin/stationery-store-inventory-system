package com.example.adprojectsa43_team1.adprojectsa43_team1.Data;

/**
 * Created by cynthianyeint on 20/1/17.
 */
import java.io.PrintWriter;
import java.io.StringWriter;

public class StackTrace {
    public static String trace(Exception ex) {
        StringWriter outStream = new StringWriter();
        ex.printStackTrace(new PrintWriter(outStream));
        return outStream.toString();
    }
}
