package com.example.adprojectsa43_team1.adprojectsa43_team1;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.Employee;

import org.w3c.dom.Text;

public class CurrentDeptHead extends AppCompatActivity implements
        NavigationView.OnNavigationItemSelectedListener{

    private DrawerLayout mDrawerLayout;
    private ActionBarDrawerToggle mToggle;

    final static String []key ={"employeeID","firstName","lastName","fullName","email","phone","address","departmentID","startDate","endDate","primaryRole","secondaryRole"};

    private String currentDeptHeadID = "";



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_delegate_dephead);

        currentDeptHeadProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        currentDeptHeadProcess();
    }

    private void currentDeptHeadProcess(){
        final SharedPreferences pref = getSharedPreferences("loginUserInfo", MODE_PRIVATE);

        //navigation drawer & toggle
        mDrawerLayout = (DrawerLayout) findViewById(R.id.activity_delegate_dephead);
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

        //current department head
        new AsyncTask<String, Void, Employee>(){
            @Override
            protected Employee doInBackground(String... params){

                return Employee.getDeptDelegatedHead(params[0]);
            }
            @Override
            protected void onPostExecute(Employee result){
                TextView currentDeptHeadName = (TextView) findViewById(R.id.txtcurrentDeptHead);
                TextView startDate = (TextView)findViewById(R.id.txtStartDate);
                TextView endDate = (TextView) findViewById(R.id.txtEndDate);
                currentDeptHeadName.setText(result.get(key[3]));
                startDate.setText(result.get(key[8]));
                endDate.setText(result.get(key[9]));
                currentDeptHeadID = result.get(key[0]);

                if(currentDeptHeadID.equals(pref.getString("currentUserEmployeeID",""))){
                    findViewById(R.id.delegationduration).setVisibility(View.GONE);
                    findViewById(R.id.startDate).setVisibility(View.GONE);
                    findViewById(R.id.txtStartDate).setVisibility(View.GONE);
                    findViewById(R.id.endDate).setVisibility(View.GONE);
                    findViewById(R.id.txtEndDate).setVisibility(View.GONE);
                    findViewById(R.id.btnCancelDelegate).setVisibility(View.GONE);
                    findViewById(R.id.btnAssignDelegateHead).setVisibility(View.VISIBLE);
                }
                else{
                    findViewById(R.id.delegationduration).setVisibility(View.VISIBLE);
                    findViewById(R.id.startDate).setVisibility(View.VISIBLE);
                    findViewById(R.id.txtStartDate).setVisibility(View.VISIBLE);
                    findViewById(R.id.endDate).setVisibility(View.VISIBLE);
                    findViewById(R.id.txtEndDate).setVisibility(View.VISIBLE);
                    findViewById(R.id.btnCancelDelegate).setVisibility(View.VISIBLE);
                    findViewById(R.id.btnAssignDelegateHead).setVisibility(View.GONE);
                }
            }

        }.execute(pref.getString("currentUserDeptID", ""));

        //assign delegate button
        Button btnAssignDelegateHead = (Button) findViewById(R.id.btnAssignDelegateHead);
        btnAssignDelegateHead.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                //select employee
                Intent intent = new Intent(getApplicationContext(), AssignDeptHead.class);
                intent.putExtra("currentDeptHeadID", currentDeptHeadID);
                startActivity(intent);
            }
        });

        //btnCancelDelegate
        Button btnCancelDelegate = (Button) findViewById(R.id.btnCancelDelegate);
        btnCancelDelegate.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v){
                new AsyncTask<Employee, Void, Void>(){
                    @Override
                    protected Void doInBackground(Employee... params){
                        Employee.removeDeptDelegateHead(currentDeptHeadID);
                        return null;
                    }
                    @Override
                    protected  void onPostExecute(Void result){
                        Intent intent = new Intent(getApplicationContext(), CurrentDeptHead.class);
                        startActivity(intent);
                    }
                }.execute();
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