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
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.Employee;

import java.util.List;

public class AssignDeptHead extends Activity {

    String currentDeptHeadID = "";
    String chosenEmployeeID = "";
    EditText searchEmployee;
    ListView employeeList;
    SimpleAdapter adapter;
    String chosenEmployee;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_choose_employee_departhead);

        assignDeptHeadProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        assignDeptHeadProcess();
    }

    private void assignDeptHeadProcess(){
        SharedPreferences pref = getSharedPreferences("loginUserInfo", MODE_PRIVATE);

        currentDeptHeadID = getIntent().getExtras().getString("currentDeptHeadID");
        Log.i("currentDeptHeadID", currentDeptHeadID);

        employeeList = (ListView) findViewById(R.id.employeeList);
        searchEmployee=(EditText)findViewById(R.id.searchEmployee);

        new AsyncTask<String, Void, List<Employee>>() {
            @Override
            protected List<Employee> doInBackground(String... params) {
                return Employee.getDepartmentEmployeeForDelegation(params[0]);
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
                        chosenEmployeeID = emp.get("employeeID").toString();
                        chosenEmployee = emp.get("fullName").toString();
                    }
                });


                searchEmployee.addTextChangedListener(new TextWatcher() {

                    @Override
                    public void onTextChanged(CharSequence cs, int arg1, int arg2, int arg3) {
                        AssignDeptHead.this.adapter.getFilter().filter(cs);

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
                Intent intent = new Intent(getApplicationContext(), DeptHeadSettings.class);
                intent.putExtra("currentDeptHeadID", currentDeptHeadID);
                intent.putExtra("chosenEmployeeID", chosenEmployeeID);
                intent.putExtra("chosenEmployee",chosenEmployee);
                startActivity(intent);

            }
        });

        //Cancel button
        Button cancelBtn = (Button) findViewById(R.id.cancelbtn);
        cancelBtn.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                Intent intent = new Intent(getApplicationContext(), CurrentDeptHead.class);
                startActivity(intent);
            }
        });
    }

}
