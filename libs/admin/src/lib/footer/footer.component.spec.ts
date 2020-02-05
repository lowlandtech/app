import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { FooterComponent } from './footer.component';
import { Component } from '@angular/core';

describe('Feature: Footer', () => {

  describe('FooterComponent', () => {
    let component: FooterComponent;
    let fixture: ComponentFixture<FooterComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ FooterComponent ]
      })
      .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(FooterComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('should create', () => {
      expect(component).toBeTruthy();
    });

  });

  describe('FooterTestComponent', ()=> {

    @Component({
      selector: 'scx-footer-test',
      template: `
        <scx-footer>
          <span title>I'm a test</span>
        </scx-footer>
      `
    })
    class FooterTestComponent {}

    let component: FooterTestComponent;
    let fixture: ComponentFixture<FooterTestComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ FooterComponent, FooterTestComponent ]
      })
      .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(FooterTestComponent);
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
