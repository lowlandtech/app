import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SidebarNavDropdownComponent } from './sidebar-nav-dropdown.component';
import { AdminModule } from '../../admin.module';
import { MockRender } from 'ng-mocks';
import { RouterTestingModule } from '@angular/router/testing';
import { StoreModule } from '@ngrx/store';

describe('SidebarNavDropdownComponent', () => {
  let component: SidebarNavDropdownComponent;
  let fixture: ComponentFixture<SidebarNavDropdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[
        AdminModule,
        RouterTestingModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = MockRender(`<scx-sidebar-nav-dropdown [link]="{
      icon: '',
      children: [{
        class: '',
        url: ''
      }]
    }" scxNavDropdown></scx-sidebar-nav-dropdown>`);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
