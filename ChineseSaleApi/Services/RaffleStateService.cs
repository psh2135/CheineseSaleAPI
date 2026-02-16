using AutoMapper;
using ChineseSaleApi.Data;
using ChineseSaleApi.DTO;
using ChineseSaleApi.DTO.ChineseSaleApi.DTOs;
using ChineseSaleApi.Models;
using ChineseSaleApi.Repositories;
using Microsoft.EntityFrameworkCore;



public interface IRaffleStateService
{
    bool IsRaffleLocked();
}


public class RaffleStateService : IRaffleStateService
{
    private readonly RaffleRepository _repo;

    public RaffleStateService(RaffleRepository repo)
    {
        _repo = repo;
    }

    public bool IsRaffleLocked()
    {
        var raffle = _repo.GetCurrentRaffle();
        return DateTime.UtcNow >= raffle.OpeningDate;
    }
}
