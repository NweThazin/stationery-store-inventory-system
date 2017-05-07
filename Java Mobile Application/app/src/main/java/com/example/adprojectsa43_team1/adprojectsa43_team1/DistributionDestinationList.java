package com.example.adprojectsa43_team1.adprojectsa43_team1;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.CollectionPoint;

import java.util.List;

public class DistributionDestinationList extends AppCompatActivity implements
        NavigationView.OnNavigationItemSelectedListener{

    private DrawerLayout mDrawerLayout;
    private ActionBarDrawerToggle mToggle;

    String collectionPointID;
    String collectionPointName;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_distribution_destination_list);
        distributionDestinationProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        distributionDestinationProcess();
    }

    private void distributionDestinationProcess(){
        collectionPointID = getIntent().getExtras().getString("collectionPointID");
        collectionPointName = getIntent().getExtras().getString("collectionPointName");

        this.setTitle("Depts at " + collectionPointName);

        //navigation drawer & toggle
        mDrawerLayout = (DrawerLayout) findViewById(R.id.activity_distribution_destination_list);
        mToggle = new ActionBarDrawerToggle(this, mDrawerLayout, R.string.open, R.string.close);

        mDrawerLayout.addDrawerListener(mToggle);
        mToggle.syncState();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);

        //distribution destinationlist
        new AsyncTask<String, Void, List<CollectionPoint>>(){
            @Override
            protected List<CollectionPoint> doInBackground(String... params){
                return CollectionPoint.getCollectionPointDeptList(params[0]);
            }
            @Override
            protected  void onPostExecute(List<CollectionPoint> result){
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), result, R.layout.distribution_destination_row,
                        new String[]{"departmentName","departmentRepName","phone"},
                        new int[]{R.id.txtDepartmentName, R.id.txtDeptRepName, R.id.txtPhone});
                ListView list = (ListView) findViewById(R.id.distributionDestinationList);
                list.setAdapter(adapter);
                list.setOnItemClickListener(new AdapterView.OnItemClickListener(){
                    public  void onItemClick(AdapterView<?>av, View v, int position, long id){
                        CollectionPoint cp = (CollectionPoint) av.getAdapter().getItem(position);
                        Intent intent = new Intent(getApplicationContext(), DisbursementList_StoreClerk.class);
                        intent.putExtra("collectionPointID", cp.get("collectionPointID").toString());
                        intent.putExtra("departmentID", cp.get("departmentID").toString());
                        intent.putExtra("departmentName", cp.get("departmentName").toString());
                        startActivity(intent);

                    }

                });
            }
        }.execute(collectionPointID);
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
