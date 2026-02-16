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
    void StartRaffle();
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
    public void StartRaffle()
    {
        var raffle = _repo.GetCurrentRaffle();

        if (raffle.IsLocked)
            throw new InvalidOperationException("Already started");

        raffle.OpeningDate = DateTime.UtcNow;

        _repo.Save();
    }


}
