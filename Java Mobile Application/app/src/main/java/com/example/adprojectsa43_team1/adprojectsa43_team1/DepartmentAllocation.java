package com.example.adprojectsa43_team1.adprojectsa43_team1;

import android.app.Dialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.support.design.widget.NavigationView;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.view.View.OnClickListener;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.RetrievalSummary;

import java.util.List;
import android.content.Context;
public class DepartmentAllocation extends AppCompatActivity implements
        NavigationView.OnNavigationItemSelectedListener{

    String itemID = "";
    String chosenDepartmentName = "";
    String chosenDepartmentID = "";
    String initialAllocatedQty ="";
    String requisition_ItemID = "";
    String updateAllocatedQty = "";

    private DrawerLayout mDrawerLayout;
    private ActionBarDrawerToggle mToggle;


    final static String []key ={"requisition_ItemID","itemNumber","itemID","description","requestedQty","inStockQty","bin","departmentName",
                        "retrievedQty","departmentID"};


    final Context context = this;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_department_allocation);

        deptAllocationProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        deptAllocationProcess();
    }

    private void deptAllocationProcess(){
        itemID = getIntent().getExtras().getString("itemID");

        //navigation drawer & toggle
        mDrawerLayout = (DrawerLayout) findViewById(R.id.activity_department_allocation);
        mToggle = new ActionBarDrawerToggle(this, mDrawerLayout, R.string.open, R.string.close);

        mDrawerLayout.addDrawerListener(mToggle);
        mToggle.syncState();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);

        //department allocation list
        new AsyncTask<String, Void, List<RetrievalSummary>>(){
            @Override
            protected List<RetrievalSummary> doInBackground(String... params){
                return RetrievalSummary.getRetrievalDeptAllocation(params[0]);
            }
            @Override
            protected void onPostExecute(List<RetrievalSummary> result){
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), result, R.layout.dept_allocation_row,
                        new String[]{"departmentName", "requestedQty","retrievedQty"},
                        new int[]{R.id.txtDeptName,R.id.txtRequestedQty, R.id.txtAllocatedQty});
                ListView list = (ListView) findViewById(R.id.deptAllocationList);
                list.setAdapter(adapter);

                list.setOnItemClickListener(new AdapterView.OnItemClickListener(){
                    public  void onItemClick(AdapterView<?>av, View v, int position, long id){
                        System.out.print("list view clicked;");
                        RetrievalSummary rs = (RetrievalSummary) av.getAdapter().getItem(position);
                        chosenDepartmentID = rs.get("departmentID");
                        chosenDepartmentName = rs.get("departmentName");
                        initialAllocatedQty = rs.get("retrievedQty");
                        requisition_ItemID = rs.get("requisition_ItemID");

                        //custom dialog
                        final Dialog dialog = new Dialog(context);
                        dialog.setContentView(R.layout.dialog);
                        TextView text = (TextView) dialog.findViewById(R.id.txtDialogTitle);
                        text.setText("Allocation for " + chosenDepartmentName);

                        EditText txtInitialAllocatedQty = (EditText) dialog.findViewById(R.id.editTxtAllocatedQty);
                        txtInitialAllocatedQty.setText(initialAllocatedQty);

                        Button dialogButton = (Button) dialog.findViewById(R.id.dialogBtnOK);
                        dialogButton.setOnClickListener(new OnClickListener() {
                            @Override
                            public void onClick(View v) {

                                EditText txtUpdateAllocatedQty = (EditText) dialog.findViewById(R.id.editTxtAllocatedQty);
                                updateAllocatedQty = txtUpdateAllocatedQty.getText().toString();
                                new AsyncTask<RetrievalSummary, Void, Void>(){
                                    @Override
                                    protected Void doInBackground(RetrievalSummary... params){
                                        RetrievalSummary.updateRetrievalSummary(chosenDepartmentID,itemID,requisition_ItemID,updateAllocatedQty);
                                        return null;
                                    }
                                    @Override
                                    protected void onPostExecute(Void result){
                                        dialog.cancel();
                                        onResume();
                                        //finish();
                                    }
                                }.execute();

                            }
                        });

                        dialog.show();



                    }

                });
            }
        }.execute(itemID);
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
        if (id == R.id.nav_retrieval) {
            Intent intent = new Intent(getApplicationContext(), RetrievalSummaryList.class);
            startActivity(intent);

        } else if (id == R.id.nav_distribution) {
            Intent intent = new Intent(getApplicationContext(), DistributionList.class);
            startActivity(intent);

        } else if (id == R.id.nav_logout) {
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
