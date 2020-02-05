import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { AsideComponent } from './aside.component';
import { Component } from '@angular/core';

describe('Feature: Aside', () => {

  describe('AsideComponent', () => {
    let component: AsideComponent;
    let fixture: ComponentFixture<AsideComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ AsideComponent ]
      })
      .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(AsideComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('should create', () => {
      expect(component).toBeTruthy();
    });

  });

  describe('AsideTestComponent', ()=> {

    @Component({
      selector: 'scx-aside-test',
      template: `
        <scx-aside>
          <span title>I'm a test</span>
        </scx-aside>
      `
    })
    class AsideTestComponent {}

    let component: AsideTestComponent;
    let fixture: ComponentFixture<AsideTestComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ AsideComponent, AsideTestComponent ]
      })
      .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(AsideTestComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

  it('should create test component', () => {
      expect(component).toBeTruthy();
    });

  it('should have projected content', () => {
      expect(
        fixture.debugElement.query(By.css('aside')).query(By.css('span'))
          .nativeElement.innerHTML
      ).toContain("I'm a test");
    });

  });

});
