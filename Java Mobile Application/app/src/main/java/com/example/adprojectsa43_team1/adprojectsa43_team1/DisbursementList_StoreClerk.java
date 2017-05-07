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

public class DisbursementList_StoreClerk extends AppCompatActivity implements
        NavigationView.OnNavigationItemSelectedListener{

    String collectionPointID = "";
    String departmentID = "";
    String departmentName = "";
    String itemNumber = "";
    String itemDescription = "";
    String initialQty = "";
    String disbursement_ItemID = "";

    final Context context = this;

    private DrawerLayout mDrawerLayout;
    private ActionBarDrawerToggle mToggle;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_disbursement_list__store_clerk);

        disbursementStoreClerkProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        disbursementStoreClerkProcess();
    }

    private void disbursementStoreClerkProcess(){
        collectionPointID = getIntent().getExtras().getString("collectionPointID");
        departmentID = getIntent().getExtras().getString("departmentID");
        departmentName = getIntent().getExtras().getString("departmentName");

        this.setTitle("Disbursement of " + departmentName);


        //navigation drawer & toggle
        mDrawerLayout = (DrawerLayout) findViewById(R.id.activity_disbursement_list__store_clerk);
        mToggle = new ActionBarDrawerToggle(this, mDrawerLayout, R.string.open, R.string.close);

        mDrawerLayout.addDrawerListener(mToggle);
        mToggle.syncState();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);

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
                ListView list = (ListView) findViewById(R.id.disbursementSCList);
                list.setAdapter(adapter);

                list.setOnItemClickListener(new AdapterView.OnItemClickListener(){
                    public  void onItemClick(AdapterView<?>av, View v, int position, long id){
                        Disbursement rs = (Disbursement) av.getAdapter().getItem(position);
                        itemNumber = rs.get("itemNumber");
                        itemDescription = rs.get("description");
                        initialQty = rs.get("qty");
                        disbursement_ItemID = rs.get("disbursement_ItemID");
                    }

                });
            }
        }.execute(departmentID);
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
