import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  templateUrl: 'manufacture.component.html'
})
export class ManufactureComponent implements OnInit {

  manufactureForm: FormGroup;

  constructor() { }

  ngOnInit(): void {
  }

  searchItem() { }
}
