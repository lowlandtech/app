import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarComponent } from './sidebar.component';
import { By } from '@angular/platform-browser';
import { Component } from '@angular/core';

describe('Feature: Sidebar', () => {

  describe('SidebarComponent', () => {
    let component: SidebarComponent;
    let fixture: ComponentFixture<SidebarComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ SidebarComponent ]
      })
      .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(SidebarComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('should create', () => {
      expect(component).toBeTruthy();
    });

  });

  describe('SidebarTestComponent', ()=> {

    @Component({
      selector: 'scx-sidebar-test',
      template: `
        <scx-sidebar>
          <span title>I'm a test</span>
        </scx-sidebar>
      `
    })
    class SidebarTestComponent {}

    let component: SidebarTestComponent;
    let fixture: ComponentFixture<SidebarTestComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ SidebarComponent, SidebarTestComponent ]
      })
      .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(SidebarTestComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

  it('should create test component', () => {
      expect(component).toBeTruthy();
    });

  it('should have projected content', () => {
      expect(
        fixture.debugElement.query(By.css('span'))
          .nativeElement.innerHTML
      ).toContain("I'm a test");
    });

  });

});
