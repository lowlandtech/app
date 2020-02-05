import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContentComponent } from './content.component';
import { Component } from '@angular/core';
import { By } from '@angular/platform-browser';

describe('Feature: Content', () => {

  describe('ContentComponent', () => {
    let component: ContentComponent;
    let fixture: ComponentFixture<ContentComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ ContentComponent ]
      })
      .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(ContentComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('should create', () => {
      expect(component).toBeTruthy();
    });

  });

  describe('ContentTestComponent', ()=> {

    @Component({
      selector: 'scx-content-test',
      template: `
        <scx-content>
          <span title>I'm a test</span>
        </scx-content>
      `
    })
    class ContentTestComponent {}

    let component: ContentTestComponent;
    let fixture: ComponentFixture<ContentTestComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ ContentComponent, ContentTestComponent ]
      })
      .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(ContentTestComponent);
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
