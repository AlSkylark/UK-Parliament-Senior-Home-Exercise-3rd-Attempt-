import { Component, OnDestroy, signal } from '@angular/core';
import { HeaderComponent } from "../header/header.component";
import { SearchComponent } from "../search/search.component";
import { FooterComponent } from "../footer/footer.component";
import { EditorComponent } from "../editor/editor.component";
import { ResultListComponent } from "../result-list/result-list.component";
import { CommonModule } from '@angular/common';
import { EditorService } from 'src/app/services/editor.service';
import { Subscription } from 'rxjs';
import { AdminPanelComponent } from "../admin-panel/admin-panel.component";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [HeaderComponent, SearchComponent, FooterComponent, EditorComponent, ResultListComponent, CommonModule, AdminPanelComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnDestroy {


  adminPanelOpen = signal(false);
  editorOpen = false;

  editorSubscription: Subscription;
  constructor(private editorService: EditorService) {
    this.editorSubscription = this.editorService.$editorOpen.subscribe(val => this.editorOpen = val);
  }

  ngOnDestroy(): void {
    this.editorSubscription.unsubscribe();
  }

  onAdminPanelClicked(val: boolean) {
    this.adminPanelOpen.set(val);
    this.editorService.closeEditor();
  }

  closeAdminPanel() {
    this.adminPanelOpen.set(false);
  }
}
