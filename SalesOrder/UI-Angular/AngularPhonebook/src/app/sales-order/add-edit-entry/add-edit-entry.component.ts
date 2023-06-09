import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, observable } from 'rxjs';
import { SalesOrderApiService } from 'src/app/Shared/services/sales-order-api.service';

@Component({
  selector: 'app-add-edit-entry',
  templateUrl: './add-edit-entry.component.html',
  styleUrls: ['./add-edit-entry.component.scss']
})
export class AddEditEntryComponent implements OnInit {

  phonebookList$! : Observable<any[]>;
  entryList$!: Observable<any[]>;

  @Input() entry:  any = {
    Description: "Kg"
   };
  Id: number = 0 ;
  Description: string = '';

  @Output() list : EventEmitter<any> = new EventEmitter<any>();

  constructor(private apiService: SalesOrderApiService) { }

  ngOnInit(): void {
    this.Description = this.entry.Name;

    this.getAllEntries();
  }

  getAllEntries() {    
    this.entryList$ = this.apiService.getEntryList();
  }

  getAllEntriesAfterAdd() {    
    this.list.emit(this.getAllEntries());
  }


  AddPhonebookEntry() {
    console.log('entry to be added: ', this.entry);
    this.apiService.addEntry(this.entry).subscribe(response => {
      this.getAllEntriesAfterAdd();
      var modalCloseBtn = document.getElementById('add-edit-model-close');
      if(modalCloseBtn) {
        modalCloseBtn.click();
      }

      var displaySuccessAlert = document.getElementById('add-success-alert');
      if(displaySuccessAlert){ displaySuccessAlert.style.display = "block"; }

      setTimeout(() => {
        if(displaySuccessAlert) { displaySuccessAlert.style.display = "none"; }
      }, 5000);
      

    }, err => {      
      var displayErrorAlert = document.getElementById('add-error-alert');      
      if(displayErrorAlert){ displayErrorAlert.style.display = "block"; }
      setTimeout(() => {
        if(displayErrorAlert) { displayErrorAlert.style.display = "none"; }
      }, 5000);
      console.log('See error: ', err); // use logs
    })

  }

  UpdatePhonebookEntry() {
    this.apiService.updateEntry(this.entry.id,this.entry).subscribe(response => {
      var modalCloseBtn = document.getElementById('add-edit-model-close');
      if(modalCloseBtn) {
        modalCloseBtn.click();
      }

      var displaySuccessAlert = document.getElementById('update-success-alert');
      if(displaySuccessAlert){ displaySuccessAlert.style.display = "block"; }
      
      setTimeout(() => {
        if(displaySuccessAlert) { displaySuccessAlert.style.display = "none"; }
      }, 5000);

    }, err => {      
      var displayErrorAlert = document.getElementById('error-alert');      
      if(displayErrorAlert){ displayErrorAlert.style.display = "block"; }
      setTimeout(() => {
        if(displayErrorAlert) { displayErrorAlert.style.display = "none"; }
      }, 5000);
      console.log('See error: ', err); // use logs
    })
  }
}
