package com.example.adprojectsa43_team1.adprojectsa43_team1;

/**
 * Created by chawtheingi on 20/1/25.
 */
import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckedTextView;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.Employee;

import java.util.List;

public class AssignDeptRepresentative extends Activity {

    String currentRepID = "";
    String chosenRepID = "";
    EditText searchEmployee;
    ListView employeeList;
    SimpleAdapter adapter;
    String chosenEmployee;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_choose_employee_rep);

        assignDeptRepProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        assignDeptRepProcess();
    }

    private void assignDeptRepProcess(){
        SharedPreferences pref = getSharedPreferences("loginUserInfo", MODE_PRIVATE);

        currentRepID = getIntent().getExtras().getString("currentRepID");

        employeeList = (ListView) findViewById(R.id.employeeList);
        searchEmployee=(EditText)findViewById(R.id.searchEmployee);


        new AsyncTask<String, Void, List<Employee>>() {
            @Override
            protected List<Employee> doInBackground(String... params) {
                return Employee.getDepartmentEmployee(params[0]);
            }
            @Override
            protected void onPostExecute(List<Employee> result) {

                adapter = new SimpleAdapter(getApplicationContext(), result,
                        R.layout.radio_list,
                        new String[]{"fullName"}, new int[]{R.id.check});

                employeeList.setAdapter(adapter);

                employeeList.setOnItemClickListener(new AdapterView.OnItemClickListener() {

                    public void onItemClick(AdapterView<?> av, View v, int position, long id) {
                        Employee emp = (Employee) av.getAdapter().getItem(position);
                        chosenRepID = emp.get("employeeID").toString();
                        chosenEmployee = emp.get("fullName").toString();
                    }
                });


                searchEmployee.addTextChangedListener(new TextWatcher() {

                    @Override
                    public void onTextChanged(CharSequence cs, int arg1, int arg2, int arg3) {
                        AssignDeptRepresentative.this.adapter.getFilter().filter(cs);

                    }
                    @Override
                    public void beforeTextChanged(CharSequence arg0, int arg1, int arg2,
                                                  int arg3) {
                    }

                    @Override
                    public void afterTextChanged(Editable arg0) {

                    }
                });
            }
        }.execute(pref.getString("currentUserDeptID", ""));

        //Assign button
        Button assignBtn = (Button) findViewById(R.id.assignbtn);
        assignBtn.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                Toast.makeText(getApplicationContext(), "Assign " + chosenEmployee + ":" + chosenRepID+ " as department representative.", Toast.LENGTH_LONG).show();

                new AsyncTask<Employee, Void, Void>(){
                    @Override
                    protected Void doInBackground(Employee... params){
                       Employee.removeDeptRep(currentRepID);
                        Employee.assignDeptRep(chosenRepID);
                        return  null;
                    }
                    @Override
                    protected void onPostExecute(Void result){
                        finish();
                    }
                }.execute();
            }
        });

        //Cancel button
        Button cancelBtn = (Button) findViewById(R.id.cancelbtn);
        cancelBtn.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                Intent intent = new Intent(getApplicationContext(), CurrentDeptRepresentative.class);
                startActivity(intent);
            }
        });
    }

}