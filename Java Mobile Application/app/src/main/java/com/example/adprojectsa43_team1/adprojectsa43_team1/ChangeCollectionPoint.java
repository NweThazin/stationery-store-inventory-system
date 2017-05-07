package com.example.adprojectsa43_team1.adprojectsa43_team1;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.CollectionPoint;
import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.Employee;

import java.util.List;

public class ChangeCollectionPoint extends AppCompatActivity {

    String chosenCollectionPointID = "";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_change_collection_point);

        changeCollectionPointProcess();
    }

    @Override
    protected void onResume(){
        super.onResume();
        changeCollectionPointProcess();
    }

    private void changeCollectionPointProcess(){
        final SharedPreferences pref = getSharedPreferences("loginUserInfo", MODE_PRIVATE);

        //Getting collection point list
        new AsyncTask<String, Void, List<CollectionPoint>>(){
            @Override
            protected List<CollectionPoint> doInBackground(String... parmas){
                return CollectionPoint.getCollectionPointList();
            }
            @Override
            protected void onPostExecute(List<CollectionPoint> result){
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), result,
                        R.layout.radio_list,
                        new String[]{"collectionPointName"}, new int[]{R.id.check});

                ListView list = (ListView) findViewById(R.id.collectionpointList);
                list.setAdapter(adapter);
                list.setOnItemClickListener(new AdapterView.OnItemClickListener() {

                    public void onItemClick(AdapterView<?> av, View v, int position, long id) {
                        CollectionPoint cp = (CollectionPoint)av.getAdapter().getItem(position);
                        chosenCollectionPointID = cp.get("collectionPointID").toString();
                        Log.i("chosenCollectionPointID",chosenCollectionPointID);
                    }
                });
            }
        }.execute();

        Button btnOk = (Button) findViewById(R.id.btnOk);
        btnOk.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                new AsyncTask<String, Void, Void>(){
                    @Override
                    protected Void doInBackground(String... params){
                        CollectionPoint.updateCollectionPoint(params[0],params[1]);
                        return  null;
                    }
                    @Override
                    protected void onPostExecute(Void result){
                        Intent intent = new Intent(getApplicationContext(), CurrentCollectionPoint.class);
                        startActivity(intent);
                        //finish();
                    }
                }.execute(chosenCollectionPointID,pref.getString("currentUserDeptID", ""));
            }
        });

        Button btnCancel = (Button) findViewById(R.id.btnCancel);
        btnCancel.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                Intent intent = new Intent(getApplicationContext(), CurrentCollectionPoint.class);
                startActivity(intent);
            }
        });
    }

}
