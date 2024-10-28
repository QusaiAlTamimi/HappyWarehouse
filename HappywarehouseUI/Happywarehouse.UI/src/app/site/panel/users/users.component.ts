import { Component, OnInit } from '@angular/core';
import { userService } from 'src/app/apis/user.service';
import { PaginationDTO } from 'src/app/core/models/paginationDTO';
import { user } from 'src/app/core/models/user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  data: user[] = []; // Full data array
  pagination: PaginationDTO<user> = new PaginationDTO<user>(); // Pagination object
  public newUser: user = { id: '', fullName: '', email: '', password: '',active:true,userName:'' };
  public showForm = false;
  public editUserId: string | null = null;
  public errortxt: string = '';

  constructor(private userService: userService) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getPaged(this.pagination.pageNumber, this.pagination.pageSize).subscribe({
      next: response => {
        this.pagination = response;
        this.data = response.list;
      },
      error: error => {
        console.log(error);
      }
    });
  }

  createUser(): void {
    if (this.newUser.id === '') {
      // Creating a new user
      this.userService.create(this.newUser).subscribe({
        next: (user) => {
          this.data.push(user);
          this.resetForm();
        },
        error: (error) => {
          console.log(error);
          this.errortxt = error.error;
        },
      });
    } else {
      // Editing an existing user
      // this.userService.update(this.newUser).subscribe({
      //   next: (updatedUser) => {
      //     const index = this.data.findIndex(u => u.id === updatedUser.id);
      //     if (index > -1) {
      //       this.data[index] = updatedUser; // Update the user in the array
      //     }
      //     this.resetForm();
      //   },
      //   error: (error) => console.log(error),
      // });
    }
  }

  editUser(user: user): void {
    this.newUser = { ...user }; // Copy the user data into newUser
    this.showForm = true; // Show the form for editing
    this.editUserId = user.id; // Set the id for tracking
  }

  deleteUser(id: string): void {
    this.userService.delete(id).subscribe({
      next: () => {
        this.data = this.data.filter(u => u.id !== id);
      },
      error: (error) => console.log(error),
    });
  }

  resetForm(): void {
    this.newUser = {
      id: '',
      fullName: '',
      email: '',
      password: '',
      active: true,
      userName: '',
    };
    this.showForm = false; // Hide the form after submission
    this.editUserId = null; // Reset the edit ID
  }

  nextPage() {
    // Go to the next page if it's not the last page
    if (this.pagination.pageNumber < this.pagination.totalPages) {
      this.pagination.pageNumber++;
      this.loadUsers();
    }
  }

  previousPage() {
    // Go to the previous page if it's not the first page
    if (this.pagination.pageNumber > 1) {
      this.pagination.pageNumber--;
      this.loadUsers();
    }
  }

  setPage(page: number) {
    // Set the current page to the specified page and update pagination
    this.pagination.pageNumber = page;
    this.loadUsers();
  }
}
