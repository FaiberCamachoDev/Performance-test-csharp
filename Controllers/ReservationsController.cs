using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Performance_test_csharp.Interfaces;
using Performance_test_csharp.Models;


namespace Performance_test_csharp.Controllers;
public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly IUserService _userService;
    private readonly IDeportiveSpace _spaceService;
    private readonly IEmailService _emailService;
    
    public ReservationsController(
        IReservationService reservationService,
        IUserService userService,
        IDeportiveSpace spaceService,
        IEmailService emailService)
    {
        _reservationService = reservationService;
        _userService = userService;
        _spaceService = spaceService;
        _emailService = emailService;
    }
    // GET reservations
    public async Task<IActionResult> Index()
    {
        var reservations = await _reservationService.GetAllAsync();
        return View(reservations);
    }

    public async Task<IActionResult> Create()
    {
        await LoadViewBags();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Reservation reservation)
    {
        if (reservation.StartTime >= reservation.FinishTime)
        {
            ModelState.AddModelError("FinishTime", "The end time must be later than the start time.");
        }
        if (ModelState.IsValid)
        {
            // validar disponibilidad usando el service
            bool isAvailable = await _reservationService.IsSpaceAvailableAsync(
                reservation.SpaceId, reservation.Date, reservation.StartTime, reservation.FinishTime);
            if (!isAvailable)
            {
                ModelState.AddModelError("", "You don't have space available");
            }
            else
            {
                //si esta disponible se guarda reserva
                reservation.Status = "Active";
                await _reservationService.CreateAsync(reservation);
                //al crearla tambien mandar email de confirmacion tal que asi
                await SendConfirmationEmail(reservation);
                //
                return  RedirectToAction(nameof(Index));
                
            }
        }

        await LoadViewBags();
        return View(reservation);
    }
    // POST reservation cancel
    [HttpPost]
    public async Task<IActionResult> Cancel(int id)
    {
        var success = await _reservationService.CancelAsync(id);

        if (!success)
            TempData["Error"] = "The reservation could not be cancelled. It may already be cancelled or not exist.";
        else
            TempData["Success"] = "Reservation cancelled successfully.";

        return RedirectToAction(nameof(Index));
    }
    
    // todo: private method para cargar los datos en los selects
    private async Task LoadViewBags()
    {
        var users = await _userService.GetAllAsync();
        var spaces = await _spaceService.GetAllAsync();
        
        ViewBag.UserId = new SelectList(users, "Id", "Name");
        ViewBag.SpaceId = new SelectList(spaces, "Id", "Name");
    }
    
    //
    private async Task SendConfirmationEmail(Reservation reservation)
    {
        // guardamos nombre de usuario y espacio
        var Res = await _reservationService.GetByIdAsync(reservation.Id);
        if (Res != null)
        {
            await _emailService.SendReservationtCreatedAsync(
                Res.User!.Email,
                Res.User.Name,
                Res.SportSpace!.Name,
                Res.Date,
                Res.StartTime,
                Res.FinishTime);
        }
    }
}