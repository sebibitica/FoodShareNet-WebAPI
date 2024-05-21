using Microsoft.AspNetCore.Mvc;
using FoodShareNet.Domain.Entities;
using FoodShareNetAPI.DTO.Order;
using OrderStatusEnum = FoodShareNet.Domain.Enums.OrderStatus;
using FoodShareNet.Application.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [ProducesResponseType(type: typeof(OrderDTO) , StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<OrderDetailsDTO>> CreateOrder(CreateOrderDTO createOrderDTO)
    {
        var order = new Order
        {
            Quantity = createOrderDTO.Quantity,
            BeneficiaryId = createOrderDTO.BeneficiaryId,
            DonationId = createOrderDTO.DonationId,
            CourierId = createOrderDTO.CourierId,
            CreationDate = createOrderDTO.CreationDate,
            OrderStatusId = createOrderDTO.OrderStatusId
        };

        var createdOrder = await _orderService.CreateOrderAsync(order);

        var orderDetails = new OrderDTO
        {
            Id = createdOrder.Id,
            BeneficiaryId = createdOrder.BeneficiaryId,
            DonationId = createdOrder.DonationId,
            CourierId = createdOrder.CourierId,
            CreationDate = createdOrder.CreationDate,
            DeliveryDate = createdOrder.DeliveryDate,
            OrderStatusId = createdOrder.OrderStatusId,
            Quantity = createOrderDTO.Quantity
        };

        return Ok(orderDetails);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrder(int? id)
    {
        var order = await _orderService.GetOrderAsync(id.Value);

        var orderDTO = new OrderDTO
        {
            Id = order.Id,
            BeneficiaryId = order.BeneficiaryId,
            BeneficiaryName = order.Beneficiary.Name,
            DonationId = order.DonationId,
            CourierId = order.CourierId,
            CourierName = order.Courier.Name,
            CreationDate = order.CreationDate,
            DeliveryDate = order.DeliveryDate,
            OrderStatusId = order.OrderStatusId,
            OrderStatusName = order.OrderStatus.Name,
            Quantity = order.Quantity
        };

        return Ok(orderDTO);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPatch("{orderId:int}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusDTO updateStatusDTO)
    {
        if (orderId != updateStatusDTO.OrderId)
        {
            return BadRequest("Mismatched Order ID");
        }

        await _orderService.UpdateOrderStatusAsync(orderId, (OrderStatusEnum)updateStatusDTO.NewStatusId);


        return NoContent();
    }
}
