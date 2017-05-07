package com.example.adprojectsa43_team1.adprojectsa43_team1;

import android.app.Activity;
import android.app.DatePickerDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.view.Window;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.Employee;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

/**
 * Created by chawtheingi on 26/1/17.
 */

public class DeptHeadSettings extends Activity implements View.OnClickListener {
    Button btnSetStart, btnSetEnd, btnSave;
    TextView txtSetStart, txtSetEnd,txtName;
    String chosenEmployee,chosenEmployeeID,currentDeptHeadID, startDate, endDate;
    ListView employeeList;
    Date dfStartDate,dfEndDate;

    private int mYear, mMonth, mDay;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_depthead_setting);

        deptHeadSettingsProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        deptHeadSettingsProcess();
    }

    private void deptHeadSettingsProcess(){
        btnSetStart = (Button) (findViewById(R.id.btnSetStart));
        btnSetEnd = (Button) (findViewById(R.id.btnSetEndDate));
        btnSave = (Button) findViewById(R.id.btnSave);

        txtSetStart = (TextView) (findViewById(R.id.txtSetStart));
        txtSetEnd = (TextView) (findViewById(R.id.txtSetEnd));
        txtName=(TextView)(findViewById(R.id.txtName));

        btnSetStart.setOnClickListener(this);
        btnSetEnd.setOnClickListener(this);
        btnSave.setOnClickListener(this);

        Intent intent=getIntent();
        chosenEmployee=intent.getStringExtra("chosenEmployee");
        chosenEmployeeID = intent.getStringExtra("chosenEmployeeID");
        currentDeptHeadID = intent.getStringExtra("currentDeptHeadID");
        txtName.setText(chosenEmployee);
        setCurrentDateOnView();
    }

    public void onClick(View v) {
        if (v == btnSetStart) {
            final Calendar c = Calendar.getInstance();
            mYear = c.get(Calendar.YEAR);
            mMonth = c.get(Calendar.MONTH);
            mDay = c.get(Calendar.DAY_OF_MONTH);
            DatePickerDialog startDatePicker = new DatePickerDialog(this, new DatePickerDialog.OnDateSetListener() {
                @Override
                public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
                    txtSetStart.setText(dayOfMonth+"/"+(month+1)+"/"+year);
                    if(isAfterEndDate(txtSetStart.getText().toString())){
                        txtSetEnd.setText(txtSetStart.getText());
                    }
                }
            },mYear,mMonth,mDay);
            startDatePicker.show();
        }
        if(v==btnSetEnd){
            final Calendar c = Calendar.getInstance();
            mYear = c.get(Calendar.YEAR);
            mMonth = c.get(Calendar.MONTH);
            mDay = c.get(Calendar.DAY_OF_MONTH);

            DatePickerDialog endDatePicker = new DatePickerDialog(this, new DatePickerDialog.OnDateSetListener() {
                @Override
                public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
                    txtSetEnd.setText(dayOfMonth+"/"+(month+1)+"/"+year);
                }
            },mYear,mMonth,mDay);

            //To disable before start date
            SimpleDateFormat format=new SimpleDateFormat("dd/M/yyyy");
            try{
                Date date=format.parse(txtSetStart.getText().toString());
                long startDateInMS=date.getTime();
                endDatePicker.getDatePicker().setMinDate(startDateInMS);
            }catch(ParseException e){
                e.printStackTrace();
            }
            endDatePicker.show();
        }
        if(v == btnSave){
            Log.i("chosenEmployee", chosenEmployee);
            Log.i("chosenEmployeeID", chosenEmployeeID); //assign
            Log.i("currentDeptHeadID",currentDeptHeadID); // remove

            final String start = txtSetStart.getText().toString();
            final String end = txtSetEnd.getText().toString();

            new AsyncTask<Employee, Void, Void>(){
                @Override
                protected Void doInBackground(Employee... params){

                    Employee.assignDeptDelegateHead(chosenEmployeeID,start,end);
                    Employee.removeDeptDelegateHead(currentDeptHeadID);
                    return null;
                }
                @Override
                protected  void onPostExecute(Void result){
                    Intent intent = new Intent(getApplicationContext(), CurrentDeptHead.class);
                    startActivity(intent);
                    //finish();
                }
            }.execute();

        }
    }
    //Show current date on first load
    public void setCurrentDateOnView() {
        txtSetStart = (TextView) findViewById(R.id.txtSetStart);
        txtSetEnd = (TextView) findViewById(R.id.txtSetEnd);
        final Calendar c = Calendar.getInstance();
        mYear = c.get(Calendar.YEAR);
        mMonth = c.get(Calendar.MONTH);
        mDay = c.get(Calendar.DAY_OF_MONTH);

        String defaultDate = mDay + "/" + (mMonth + 1) + "/" + mYear;

        txtSetStart.setText(defaultDate);
        txtSetEnd.setText(txtSetStart.getText());


    }
    public boolean isAfterEndDate(String date){
        try{
            SimpleDateFormat format=new SimpleDateFormat("dd/M/yyyy");
            Date startDate=format.parse(date);
            Date endDate=format.parse(txtSetEnd.getText().toString());
            if(startDate.after(endDate)){
                return true;

            }else{
                return false;
            }

        }catch(ParseException e){
            return false;

        }
    }


}
