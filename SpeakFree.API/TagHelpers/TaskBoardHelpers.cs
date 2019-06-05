using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpeakFree.API.Models;
using SpeakFree.API.Services;
using SpeakFree.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakFree.API.TagHelpers
{
	public static class TaskBoardHelpers
	{
		/// <summary>
		/// Создание списка карточек
		/// </summary>
		/// <param name="html"></param>
		/// <param name="messages"></param>
		/// <returns></returns>
		public static HtmlString CreateTaskRow(this IHtmlHelper html, TaskViewModel model)
		{
			IEnumerable<Message> messages = model.Tasks;
			int counter = 0;
			string row = string.Empty;
			StringBuilder cards = new StringBuilder();
			StringBuilder cardRows = null;
			StringBuilder rows = new StringBuilder();

			cardRows = new StringBuilder();
			cardRows.Append("<div class='row'>");

			for (int i = 0; i < messages.Count(); i++)
			{
				if (counter == 2)
				{
					counter = 0;
				}
				string author = messages.ToList()[i].Author != null ?
					messages.ToList()[i].Author?.Surename + " " + messages.ToList()[i]?.Author?.Name + " " + messages.ToList()[i].Author?.Patronymic
					:
					"Аноним";

				cards.AppendFormat(@"
										 <div class='col-xl-6'>
								<div class='card border-left-3 border-left-success-400 rounded-left-0'>
									<div class='card-body'>
										<div class='d-sm-flex align-item-sm-center flex-sm-nowrap'>
											<div>
												<h6><a href='task_manager_detailed.html'>#{0}. {1}</a></h6>
												<p class='mb-3'>{2}</p>

							                	<a href='#'>
							                		<img src='../../../../global_assets/images/placeholders/placeholder.jpg' class='rounded-circle' width='36' height='36' alt=''>
						                		</a>
							                	<a href='#'>
							                		<img src='../../../../global_assets/images/placeholders/placeholder.jpg' class='rounded-circle' width='36' height='36' alt=''>
						                		</a>
							                	<a href='#'>
							                		<img src='../../../../global_assets/images/placeholders/placeholder.jpg' class='rounded-circle' width='36' height='36' alt=''>
						                		</a>
							                	<a href='#' class='btn btn-icon bg-transparent border-slate-300 text-slate rounded-round border-dashed'><i class='icon-plus22'></i></a>
											</div>

											<ul class='list list-unstyled mb-0 mt-3 mt-sm-0 ml-auto'>
												<li><span class='text-muted'>{3}</span></li>
												<li class='dropdown'>
							                		Priority: &nbsp; 
													<a href='#' class='badge bg-success-400 align-top dropdown-toggle' data-toggle='dropdown'>Normal</a>
													<div class='dropdown-menu dropdown-menu-right'>
														<a href='#' class='dropdown-item'><span class='badge badge-mark mr-2 border-danger'></span> Blocker</a>
														<a href='#' class='dropdown-item'><span class='badge badge-mark mr-2 border-warning-400'></span> High priority</a>
														<a href='#' class='dropdown-item active'><span class='badge badge-mark mr-2 border-success'></span> Normal priority</a>
														<a href='#' class='dropdown-item'><span class='badge badge-mark mr-2 border-grey-300'></span> Low priority</a>
													</div>
												</li>
												<li><a href='#'>{4}</a></li>
											</ul>
										</div>
									</div>

									<div class='card-footer d-sm-flex justify-content-sm-between align-items-sm-center'>
										<span>Due: <span class='font-weight-semibold'>23 hours</span></span>

										<ul class='list-inline mb-0 mt-2 mt-sm-0'>
											<li class='list-inline-item dropdown'>
												<a href='#' class='text-default dropdown-toggle' data-toggle='dropdown'>Open</a>

												<div class='dropdown-menu dropdown-menu-right'>
													<a href='#' class='dropdown-item active'>Open</a>
													<a href='#' class='dropdown-item'>On hold</a>
													<a href='#' class='dropdown-item'>Resolved</a>
													<a href='#' class='dropdown-item'>Closed</a>
													<div class='dropdown-divider'></div>
													<a href='#' class='dropdown-item'>Dublicate</a>
													<a href='#' class='dropdown-item'>Invalid</a>
													<a href='#' class='dropdown-item'>Wontfix</a>
												</div>
											</li>
											<li class='list-inline-item dropdown'>
												<a href='#' class='text-default dropdown-toggle' data-toggle='dropdown'><i class='icon-menu7'></i></a>

												<div class='dropdown-menu dropdown-menu-right'>
													<a href='#' class='dropdown-item'><i class='icon-alarm-add'></i> Check in</a>
													<a href='#' class='dropdown-item'><i class='icon-attachment'></i> Attach screenshot</a>
													<a href='#' class='dropdown-item'><i class='icon-rotate-ccw2'></i> Reassign</a>
													<div class='dropdown-divider'></div>
													<a href='#' class='dropdown-item'><i class='icon-pencil7'></i> Edit task</a>
													<a href='#' class='dropdown-item'><i class='icon-cross2'></i> Remove</a>
												</div>
											</li>
										</ul>
									</div>
								</div>
							</div>",
							messages.ToList()[i].Id,
							messages.ToList()[i].Title,
							messages.ToList()[i].Text,
							messages.ToList()[i].CreatedAt,
							author
							);
				counter++;
			}

			cardRows.Append(cards);
			cardRows.Append("</div>");
			rows.Append(cardRows);

			return new HtmlString(rows.ToString());
		}
	}
}
