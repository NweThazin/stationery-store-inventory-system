package com.example.adprojectsa43_team1.adprojectsa43_team1;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.Employee;

public class CurrentDeptRepresentative extends AppCompatActivity implements
        NavigationView.OnNavigationItemSelectedListener{

    private DrawerLayout mDrawerLayout;
    private ActionBarDrawerToggle mToggle;

    final static String []key ={"employeeID","firstName","lastName","fullName","email","phone","address","departmentID"};

    private String currentRepID = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_current_representative);

        currentDeptRepresentativeProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        currentDeptRepresentativeProcess();
    }

    private void currentDeptRepresentativeProcess(){
        SharedPreferences pref = getSharedPreferences("loginUserInfo", MODE_PRIVATE);

        //navigation drawer & toggle
        mDrawerLayout = (DrawerLayout) findViewById(R.id.activity_change_representative);
        mToggle = new ActionBarDrawerToggle(this, mDrawerLayout, R.string.open, R.string.close);

        mDrawerLayout.addDrawerListener(mToggle);
        mToggle.syncState();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_depthead_view);
        navigationView.setNavigationItemSelectedListener(this);
        //hidding menu item
        Menu nav_Menu = navigationView.getMenu();
        if(pref.getString("primaryRole","").equals("Dept Head")){
            nav_Menu.findItem(R.id.nav_disbursement).setVisible(false);
        }
        else if(pref.getString("delegatedRole","").equals("Employee Head")){
            nav_Menu.findItem(R.id.nav_delegate_head).setVisible(false);
            nav_Menu.findItem(R.id.nav_change_representative).setVisible(false);
            nav_Menu.findItem(R.id.nav_disbursement).setVisible(false);
        }
        else if(pref.getString("delegatedRole","").equals("Employee RepHead")){
            nav_Menu.findItem(R.id.nav_delegate_head).setVisible(false);
        }
        else if(pref.getString("delegatedRole","").equals("Employee Rep")){
            nav_Menu.findItem(R.id.nav_approve_requisitions).setVisible(false);
            nav_Menu.findItem(R.id.nav_change_representative).setVisible(false);
            nav_Menu.findItem(R.id.nav_delegate_head).setVisible(false);
        }

        //current representative
        new AsyncTask<String, Void, Employee>(){
            @Override
            protected Employee doInBackground(String... params){

                return Employee.getDeptRepresentative(params[0]);
            }
            @Override
            protected void onPostExecute(Employee result){
                TextView tv = (TextView) findViewById(R.id.txtcurrentDeptRep);
                tv.setText(result.get(key[3]));
                currentRepID = result.get(key[0]);
            }

        }.execute(pref.getString("currentUserDeptID", ""));

        //change representative button
        Button changerepBtn = (Button) findViewById(R.id.changerep);
        changerepBtn.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                Intent intent = new Intent(getApplicationContext(), AssignDeptRepresentative.class);
                intent.putExtra("currentRepID", currentRepID);
                startActivity(intent);
            }
        });
    }


    @Override
    public boolean onOptionsItemSelected(MenuItem item){
        if(mToggle.onOptionsItemSelected(item)){
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        int id = item.getItemId();
        if(id == R.id.nav_approve_requisitions){
            Intent intent = new Intent(getApplicationContext(), ApproveRequisitions.class);
            startActivity(intent);
        } else if( id == R.id.nav_change_collectionpoint){
            Intent intent = new Intent(getApplicationContext(), CurrentCollectionPoint.class);
            startActivity(intent);
        } else if( id == R.id.nav_change_representative){
            Intent intent = new Intent(getApplicationContext(), CurrentDeptRepresentative.class);
            startActivity(intent);
        } else if( id == R.id.nav_delegate_head){
            Intent intent = new Intent(getApplicationContext(), CurrentDeptHead.class);
            startActivity(intent);
        }else if( id == R.id.nav_disbursement){
            Intent intent = new Intent(getApplicationContext(), DisbursementList_DeptRep.class);
            startActivity(intent);
        } else if( id == R.id.nav_logout){
            SharedPreferences sp=getSharedPreferences("loginUserInfo",MODE_PRIVATE);
            SharedPreferences.Editor e=sp.edit();
            e.clear();
            e.commit();

            Intent intent = new Intent(getApplicationContext(), LoginActivity.class);
            startActivity(intent);
        }
        return true;
    }

}
