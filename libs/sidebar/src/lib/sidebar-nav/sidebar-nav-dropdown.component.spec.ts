import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarNavDropdownComponent } from './sidebar-nav-dropdown.component';

describe('SidebarNavDropdownComponent', () => {
  let component: SidebarNavDropdownComponent;
  let fixture: ComponentFixture<SidebarNavDropdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SidebarNavDropdownComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarNavDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
