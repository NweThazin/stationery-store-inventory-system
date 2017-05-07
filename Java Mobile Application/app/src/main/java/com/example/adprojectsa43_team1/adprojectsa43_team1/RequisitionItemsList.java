package com.example.adprojectsa43_team1.adprojectsa43_team1;

import android.app.Dialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;
import android.content.Context;

import com.example.adprojectsa43_team1.adprojectsa43_team1.Data.RequisitionItems;

import org.w3c.dom.Text;

import java.util.List;

public class RequisitionItemsList extends AppCompatActivity {

    String requisitionID = "";
    String reason = "";

    final Context context = this;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_requisition_items);

        requisitionItemsListProcess();
    }

    @Override
    protected void onResume() {
        super.onResume();
        requisitionItemsListProcess();
    }

    private void requisitionItemsListProcess(){
        requisitionID = getIntent().getExtras().getString("requisitionID");

        //Getting requisition item list
        new AsyncTask<String, Void, List<RequisitionItems>>(){
            @Override
            protected List<RequisitionItems> doInBackground(String... params){
                return RequisitionItems.getRequisitionItems(params[0]);
            }
            @Override
            protected void onPostExecute(List<RequisitionItems> result){
                SimpleAdapter adapter = new SimpleAdapter(getApplicationContext(), result, R.layout.requisition_items_row,
                        new String[]{"description","requestedQty","uom"}, new int[]{R.id.txtDescription,R.id.txtRequestedQty,R.id.txtUom});

                ListView list = (ListView) findViewById(R.id.requisitionItemsList);
                list.setAdapter(adapter);
            }
        }.execute(requisitionID);

        //approve requisition
        Button btnApprove = (Button) findViewById(R.id.btnApprove);
        btnApprove.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                Toast.makeText(getApplicationContext(), "Approve requisition: " + requisitionID, Toast.LENGTH_LONG).show();
                new AsyncTask<RequisitionItems, Void, Void>(){
                    @Override
                    protected Void doInBackground(RequisitionItems... params){
                        RequisitionItems.approveRequisition(requisitionID);
                        return null;
                    }
                    @Override
                    protected void onPostExecute(Void result){
                        Intent intent = new Intent(getApplicationContext(), ApproveRequisitions.class);
                        startActivity(intent);
                    }
                }.execute();
            }
        });

        //reject requisition
        Button btnReject = (Button) findViewById(R.id.btnReject);
        btnReject.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                Toast.makeText(getApplicationContext(), "Reject requisition: " + requisitionID, Toast.LENGTH_LONG).show();

                final Dialog rejectDialog = new Dialog(context);
                rejectDialog.setContentView(R.layout.reject_dialog);
                TextView text = (TextView) rejectDialog.findViewById(R.id.txtDialogTitle);
                text.setText("Reject Reason: ");

                final EditText editTxtReason = (EditText) rejectDialog.findViewById(R.id.editTxtReason);

                Button btn = (Button) rejectDialog.findViewById(R.id.dialogBtnReject);
                btn.setOnClickListener(new View.OnClickListener(){
                    @Override
                    public void onClick(View v){
                        Log.i("status: ", "dialog btn reject");
                        reason = editTxtReason.getText().toString();
                        Log.i("reason:", reason);

                        new AsyncTask<String, Void, Void>(){
                            @Override
                            protected Void doInBackground(String... params){
                                RequisitionItems.rejectRequisition(params[0],params[1]);
                                return null;
                            }
                            @Override
                            protected void onPostExecute(Void result){
                                rejectDialog.cancel();
                                Intent intent = new Intent(getApplicationContext(), ApproveRequisitions.class);
                                startActivity(intent);
                            }
                        }.execute(requisitionID,reason);
                    }
                });
                rejectDialog.show();

            }
        });
    }


}
