import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarNavTitleComponent } from './sidebar-nav-title.component';

describe('SidebarNavTitleComponent', () => {
  let component: SidebarNavTitleComponent;
  let fixture: ComponentFixture<SidebarNavTitleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SidebarNavTitleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarNavTitleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
