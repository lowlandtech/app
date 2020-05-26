import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { MainComponent } from './main.component';
import { Component } from '@angular/core';

describe('Feature: Main', () => {

  describe('MainComponent', () => {
    let component: MainComponent;
    let fixture: ComponentFixture<MainComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ MainComponent ]
      })
      .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(MainComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('should create', () => {
      expect(component).toBeTruthy();
    });

  });

  describe('MainTestComponent', ()=> {

    @Component({
      selector: 'scx-sidebar-main-test',
      template: `
        <scx-main>
          <span title>I'm a test</span>
        </scx-main>
      `
    })
    class MainTestComponent {}

    let component: MainTestComponent;
    let fixture: ComponentFixture<MainTestComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ MainComponent, MainTestComponent ]
      })
      .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(MainTestComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

  it('should create test component', () => {
      expect(component).toBeTruthy();
    });

  it('should have projected content', () => {
      expect(
        fixture.debugElement.query(By.css('.main')).query(By.css('span'))
          .nativeElement.innerHTML
      ).toContain("I'm a test");
    });

  });

});
