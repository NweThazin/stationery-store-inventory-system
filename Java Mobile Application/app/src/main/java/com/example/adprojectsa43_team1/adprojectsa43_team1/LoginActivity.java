package com.example.adprojectsa43_team1.adprojectsa43_team1;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.Preference;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.widget.Toast;


import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.LoginUser;

public class LoginActivity extends AppCompatActivity {

    final static String []loginKey = {"userID","employeeID","departmentID","departmentName","departmentCode","primaryRole","delegatedRole"};

    private String currentUserID = "";
    private String currentUserEmployeeID = "";
    private String currentUserDeptID = "";
    private String primaryRole = "";
    private String delegatedRole = "";

    SharedPreferences sp;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        loginProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        loginProcess();
    }

    private void loginProcess(){
        Button loginBtn = (Button) findViewById(R.id.loginBtn);
        sp = getSharedPreferences("loginUserInfo", MODE_PRIVATE);


        /*if(sp.contains("userID")){
            startActivity(new Intent(LoginActivity.this, ApproveRequisitions.class));
            finish();
        }*/

        loginBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                //getting username & password
                EditText username = (EditText) findViewById(R.id.username);
                EditText password = (EditText) findViewById(R.id.password);

                if(username.getText().toString().equals("") || password.getText().toString().equals("")){
                    Log.i("status", "empty ");
                    AlertDialog.Builder builder1=new AlertDialog.Builder(LoginActivity.this);
                    builder1.setMessage("Enter username & password");
                    builder1.setPositiveButton("OK",new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog,int id) {
                            onResume();
                        }
                    });
                    builder1.show();

                }
                else{
                    new AsyncTask<String, Void, LoginUser>(){
                        @Override
                        protected LoginUser doInBackground(String... params){

                            return LoginUser.getLoginInfo(params[0],params[1]);
                        }
                        @Override
                        protected void onPostExecute(LoginUser result){
                            Log.i("result", result.toString());

                            currentUserID = result.get(loginKey[0]);
                            currentUserEmployeeID = result.get(loginKey[1]);
                            currentUserDeptID = result.get(loginKey[2]);
                            primaryRole = result.get(loginKey[5]);
                            delegatedRole = result.get(loginKey[6]);

                            Log.i("primaryRole", primaryRole);
                            Log.i("delegatedRole", delegatedRole);

                            SharedPreferences.Editor e = sp.edit();
                            e.putString("currentUserID",currentUserID);
                            e.putString("currentUserEmployeeID",currentUserEmployeeID);
                            e.putString("currentUserDeptID",currentUserDeptID);
                            e.putString("primaryRole",primaryRole);
                            e.putString("delegatedRole", delegatedRole);
                            e.commit();

                            if(primaryRole.equals("Dept Head")){
                                Intent intent = new Intent(getApplicationContext(), ApproveRequisitions.class);
                                startActivity(intent);
                            }
                            else if(delegatedRole.equals("Employee Head")){
                                Intent intent = new Intent(getApplicationContext(), ApproveRequisitions.class);
                                startActivity(intent);
                            }
                            else if(delegatedRole.equals("Employee RepHead")){
                                Intent intent = new Intent(getApplicationContext(), ApproveRequisitions.class);
                                startActivity(intent);
                            }
                            else if(delegatedRole.equals("Employee Rep")){
                                Intent intent = new Intent(getApplicationContext(), CurrentCollectionPoint.class);
                                startActivity(intent);
                            }
                            else if(primaryRole.equals("Store Clerk")){ //store clerk
                                Intent intent = new Intent(getApplicationContext(), RetrievalSummaryList.class);
                                startActivity(intent);
                            }
                        }
                    }.execute(username.getText().toString(), password.getText().toString());
                }
            }
        });
    }
}