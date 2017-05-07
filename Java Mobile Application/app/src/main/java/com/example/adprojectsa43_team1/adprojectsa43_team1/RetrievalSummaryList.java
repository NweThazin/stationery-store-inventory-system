package com.example.adprojectsa43_team1.adprojectsa43_team1;

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
import android.widget.ListView;
import android.widget.SimpleAdapter;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.RetrievalSummary;

import java.util.List;

public class RetrievalSummaryList extends AppCompatActivity implements
        NavigationView.OnNavigationItemSelectedListener{

    private DrawerLayout mDrawerLayout;
    private ActionBarDrawerToggle mToggle;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_retrieval_summary);
        retrievalSummaryListProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        retrievalSummaryListProcess();
    }

    private void retrievalSummaryListProcess(){
        //navigation drawer & toggle
        mDrawerLayout = (DrawerLayout) findViewById(R.id.activity_retrieval_summary);
        mToggle = new ActionBarDrawerToggle(this, mDrawerLayout, R.string.open, R.string.close);

        mDrawerLayout.addDrawerListener(mToggle);
        mToggle.syncState();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);

        //retrieval list
        new AsyncTask<String, Void, List<RetrievalSummary>>(){
            @Override
            protected List<RetrievalSummary> doInBackground(String... params){
                return RetrievalSummary.getRetrievalSummaryList();
            }
            @Override
            protected void onPostExecute(List<RetrievalSummary> result){
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(),result, R.layout.retrieval_row,
                        new String[]{"itemNumber","description","bin","totalRetrievedQty"},
                        new int[]{R.id.txtItemNumber,R.id.txtItemDescription,R.id.txtBin,R.id.txtTotalRetrievedQty});
                ListView list = (ListView) findViewById(R.id.retrievalList);
                list.setAdapter(adapter);
                list.setOnItemClickListener(new AdapterView.OnItemClickListener(){
                    public  void onItemClick(AdapterView<?>av, View v, int position, long id){
                        RetrievalSummary rs = (RetrievalSummary) av.getAdapter().getItem(position);
                        Intent intent = new Intent(getApplicationContext(), DepartmentAllocation.class);
                        intent.putExtra("itemID", rs.get("itemID").toString());
                        startActivity(intent);
                    }

                });
            }
        }.execute();
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
