import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { PaginationService } from './pagination.service';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {

  constructor(private paginationService: PaginationService) { }

  ngOnInit(): void {
    this.paginationService.setPaginationComponent(this);
  }

  updatePagination(selectedPage : number, total_pages : number): void{
    let pagination: HTMLElement = $("#pagination-container").get(0) as HTMLElement; 
    let paginationKod: string = "";
    let pagesKod: string = "";
    let previousKod: string = "";
    let nextKod: string = "";

    for (let i: number = selectedPage; i <= total_pages; i++) {
      if (i === selectedPage) {
        if (selectedPage > 1) {
          pagesKod += `<li class="page-item"><a class="page-link" onclick="changingFilters(1)" href="#1">1</a></li>`;
          if (selectedPage > 3) {
            pagesKod += `<li class="page-item ellipsis"><a class="page-link">···</a></li>`;
            pagesKod += `<li class="page-item"><a class="page-link" onclick="changingFilters(${i - 1})" href="#${i - 1}">${i - 1}</a></li>`;
          } else if (selectedPage > 2) {
            pagesKod += `<li class="page-item"><a class="page-link" onclick="changingFilters(${i - 1})" href="#${i - 1}">${i - 1}</a></li>`;
          }
        }
        pagesKod += `<li class="page-item active"><a class="page-link" onclick="changingFilters(${i})" href="#${i}">${i}</a></li>`;
      } else if (i < selectedPage + 3) {
        pagesKod += `<li class="page-item"><a class="page-link" onclick="changingFilters(${i})" href="#${i}">${i}</a></li>`;
      } else if (i < selectedPage + 4 && (selectedPage + 3) == total_pages) {
        pagesKod += `<li class="page-item"><a class="page-link" onclick="changingFilters(${total_pages})"  href="#${total_pages}">${total_pages}</a></li>`;
        break;
      } else {
        pagesKod += `<li class="page-item ellipsis"><a class="page-link">···</a></li>`;
        pagesKod += `<li class="page-item"><a class="page-link" onclick="changingFilters(${total_pages})"  href="#${total_pages}">${total_pages}</a></li>`;
        break;
      }
    }

    if (selectedPage > 1) {
      previousKod = `
        <li class="page-item page-prev">
          <a class="page-link" onclick="changingFilters(${selectedPage - 1})"  href="#${selectedPage - 1}" aria-label="Previous">
            <i class="fa-solid fa-circle-arrow-left"></i>
            <p>Prev</p>
          </a>
        </li>
      `;
    }

    if (selectedPage < total_pages) {
      nextKod = `
        <li class="page-item page-next">
          <a class="page-link" onclick="changingFilters(${selectedPage + 1})" href="#${selectedPage + 1}" aria-label="Next">
            <p>Next</p>
            <i class="fa-solid fa-circle-arrow-right"></i>
          </a>
        </li>
      `;
    }

    paginationKod = `
      <nav aria-label="Page navigation example">
        <ul class="pagination-component pagination-color">
          ${previousKod}
          ${pagesKod}
          ${nextKod}
        </ul>
      </nav>
    `;
    pagination.innerHTML = paginationKod;
  }
}
