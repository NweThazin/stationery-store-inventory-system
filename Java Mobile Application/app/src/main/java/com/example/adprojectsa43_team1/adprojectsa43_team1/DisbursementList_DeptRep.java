package com.example.adprojectsa43_team1.adprojectsa43_team1;

import android.app.Dialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.support.design.widget.NavigationView;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.Disbursement;

import java.util.List;

public class DisbursementList_DeptRep extends AppCompatActivity implements
        NavigationView.OnNavigationItemSelectedListener{

    String itemID = "";
    String departmentID = "";
    String departmentName = "";
    String itemNumber = "";
    String itemDescription = "";
    String initialQty = "";
    String updatedQty ="";
    String disbursement_ItemID = "";
    String disbursementID = "";

    final Context context = this;

    private DrawerLayout mDrawerLayout;
    private ActionBarDrawerToggle mToggle;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_disbursement_list__dept_rep);

        disbursementDepRepProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        disbursementDepRepProcess();
    }

    private void disbursementDepRepProcess(){
        SharedPreferences pref = getSharedPreferences("loginUserInfo", MODE_PRIVATE);
        departmentID = pref.getString("currentUserDeptID", "");
        departmentName = pref.getString("currentUserDeptID", "");

        this.setTitle("Disbursement List");
        //navigation drawer & toggle
        mDrawerLayout = (DrawerLayout) findViewById(R.id.activity_disbursement_list__dept_rep);
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

        //disbursement list
        new AsyncTask<String, Void, List<Disbursement>>(){
            @Override
            protected List<Disbursement> doInBackground(String... params){
                return Disbursement.getDisbursementList(params[0]);
            }
            @Override
            protected void onPostExecute(List<Disbursement> result){
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), result, R.layout.disbursement_storeclerk_row,
                        new String[]{"itemNumber","description","qty"},
                        new int[]{R.id.txtItemNumber, R.id.txtItemDescription, R.id.txtQty});
                ListView list = (ListView) findViewById(R.id.disbursementDRList);
                list.setAdapter(adapter);

                list.setOnItemClickListener(new AdapterView.OnItemClickListener(){
                    public  void onItemClick(AdapterView<?>av, View v, int position, long id){
                        Disbursement rs = (Disbursement) av.getAdapter().getItem(position);
                        itemNumber = rs.get("itemNumber");
                        itemDescription = rs.get("description");
                        initialQty = rs.get("qty");
                        disbursement_ItemID = rs.get("disbursement_ItemID");
                        itemID = rs.get("itemID");
                        disbursementID = rs.get("disbursementID");

                        //custom dialog
                        final Dialog dialog = new Dialog(context);
                        dialog.setContentView(R.layout.dialog);
                        TextView text = (TextView) dialog.findViewById(R.id.txtDialogTitle);
                        text.setText("Qty of " + itemDescription + " to be disbursed");

                        EditText txtInitialAllocatedQty = (EditText) dialog.findViewById(R.id.editTxtAllocatedQty);
                        txtInitialAllocatedQty.setText(initialQty);

                        Button dialogButton = (Button) dialog.findViewById(R.id.dialogBtnOK);
                        dialogButton.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View v) {

                                EditText txtUpdateAllocatedQty = (EditText) dialog.findViewById(R.id.editTxtAllocatedQty);
                                updatedQty = txtUpdateAllocatedQty.getText().toString();

                                new AsyncTask<String, Void, Void>(){
                                    @Override
                                    protected Void doInBackground(String... params){
                                        Disbursement.updateDisbursement(params[0], params[1], params[2], params[3]);
                                        return null;
                                    }
                                    @Override
                                    protected void onPostExecute(Void result){
                                        dialog.cancel();
                                        onResume();
                                        //finish();
                                    }
                                }.execute(departmentID, disbursementID, itemID, updatedQty);

                            }
                        });

                        dialog.show();
                    }

                });

            }
        }.execute(departmentID);

        Button btnConfirmDisbursement = (Button)findViewById(R.id.btnConfirmDisbursement);
        btnConfirmDisbursement.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                new AsyncTask<String, Void, Void>() {
                    @Override
                    protected Void doInBackground(String... params) {
                        Disbursement.confirmDisbursement(params[0]);
                        return null;
                    }
                    @Override
                    protected void onPostExecute(Void result) {
                        //onResume();
                        finish();
                    }
                }.execute(departmentID);

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
        } else if( id == R.id.nav_disbursement){
            Intent intent = new Intent(getApplicationContext(), DisbursementList_DeptRep.class);
            startActivity(intent);
        }else if( id == R.id.nav_logout){
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

